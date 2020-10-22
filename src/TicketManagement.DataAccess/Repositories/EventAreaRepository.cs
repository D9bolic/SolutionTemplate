using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : IEventAreaRepository
    {
        internal const decimal MultiplierForCountingCents = 100m;

        private const string IdField = "Id";
        private const string EventIdField = "EventId";
        private const string DescriptionField = "Description";
        private const string CoordXField = "CoordX";
        private const string CoordYField = "CoordY";
        private const string PriceField = "Price";

        private const string IdParameter = "@id";
        private const string DescriptionParameter = "@description";
        private const string PriceParameter = "@price";

        private const string SelectAllSqlExpression = @"select Id, EventId, Description, CoordX, CoordY, Price 
                                                        from EventArea";

        private const string SelectByIdSqlExpression = @"select Id, EventId, Description, CoordX, CoordY, Price 
                                                         from EventArea 
                                                         where Id = @id";

        private const string UpdateSqlExpression = @"update EventArea 
                                                     set Description = @description, Price = @price 
                                                     where Id = @id";

        private readonly string _connectionString;

        public EventAreaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<EventAreaDomain>> GetAllAsync()
        {
            // variable for return
            var eventAreas = new List<EventAreaDomain>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectAllSqlExpression, connection))
                {
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        eventAreas.Add(new EventAreaDomain
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField)),
                            EventId = dataReader.GetInt32(dataReader.GetOrdinal(EventIdField)),
                            Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField)),
                            CoordX = dataReader.GetInt32(dataReader.GetOrdinal(CoordXField)),
                            CoordY = dataReader.GetInt32(dataReader.GetOrdinal(CoordYField)),
                            Price = dataReader.GetDecimal(dataReader.GetOrdinal(PriceField)) / MultiplierForCountingCents,
                        });
                    }
                }
            }

            return eventAreas;
        }

        public async Task<EventAreaDomain> GetByIdAsync(int eventAreaId)
        {
            // variable for return
            var eventArea = new EventAreaDomain();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectByIdSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, eventAreaId);
                    command.Parameters.Add(id);

                    var dataReader = await command.ExecuteReaderAsync();
                    dataReader.Read();

                    eventArea.Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField));
                    eventArea.EventId = dataReader.GetInt32(dataReader.GetOrdinal(EventIdField));
                    eventArea.Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField));
                    eventArea.CoordX = dataReader.GetInt32(dataReader.GetOrdinal(CoordXField));
                    eventArea.CoordY = dataReader.GetInt32(dataReader.GetOrdinal(CoordYField));
                    eventArea.Price = dataReader.GetDecimal(dataReader.GetOrdinal(PriceField)) / MultiplierForCountingCents;
                }
            }

            return eventArea;
        }

        public async Task<EventAreaDomain> UpdateAsync(EventAreaDomain eventArea)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(UpdateSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(PriceParameter, eventArea?.Price * MultiplierForCountingCents),
                            new SqlParameter(DescriptionParameter, eventArea?.Description),
                            new SqlParameter(IdParameter, eventArea?.Id),
                        });

                    await command.ExecuteNonQueryAsync();
                    var updatedItem = await GetByIdAsync((int)eventArea?.Id);
                    return updatedItem;
                }
            }
        }
    }
}