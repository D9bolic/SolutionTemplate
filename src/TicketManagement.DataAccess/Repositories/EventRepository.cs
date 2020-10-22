using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Enums;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private const string IdField = "Id";
        private const string LayoutIdField = "LayoutId";
        private const string NameField = "Name";
        private const string DescriptionField = "Description";
        private const string StartDateTimeField = "StartDateTime";
        private const string FinishDateTimeField = "FinishDateTime";

        private const string EventIdParameter = "@eventId";
        private const string LayoutIdParameter = "@layoutId";
        private const string NameParameter = "@name";
        private const string StateParameter = "@state";
        private const string PriceParameter = "@price";
        private const string DescriptionParameter = "@description";
        private const string StartDateTimeParameter = "@startDt";
        private const string FinishDateTimeParameter = "@finishDt";

        private const string CreateSqlStoredProc = "AddNewEvent";
        private const string UpdateSqlStoredProc = "UpdateEvent";
        private const string DeleteSqlStoredProc = "DeleteEvent";
        private const string SelectAllSqlExpression = @"select Id, LayoutId, Name, Description, StartDateTime, FinishDateTime 
                                                        from Event";

        private const string SelectByIdSqlExpression = @"select Id, LayoutId, Name, Description, StartDateTime, FinishDateTime 
                                                         from Event 
                                                         where Id = @eventId";

        private readonly string _connectionString;

        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<EventDomain> CreateAsync(EventDomain item, decimal price)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(CreateSqlStoredProc, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(NameParameter, item?.Name),
                            new SqlParameter(DescriptionParameter, item?.Description),
                            new SqlParameter(StartDateTimeParameter, item?.StartDateTime),
                            new SqlParameter(FinishDateTimeParameter, item?.FinishDateTime),
                            new SqlParameter(LayoutIdParameter, item?.LayoutId),

                            new SqlParameter(PriceParameter, price * EventAreaRepository.MultiplierForCountingCents),
                            new SqlParameter(StateParameter, EventSeatState.Free),
                            new SqlParameter
                            {
                                ParameterName = EventIdParameter,
                                Value = DbType.Int32,
                                Direction = ParameterDirection.Output,
                            },
                        });

                    command.CommandType = CommandType.StoredProcedure;
                    await command.ExecuteNonQueryAsync();

                    var returnedId = (int)command.Parameters[EventIdParameter].Value;
                    var insertedItem = await GetByIdAsync(returnedId);
                    return insertedItem;
                }
            }
        }

        public async Task DeleteAsync(int eventId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(DeleteSqlStoredProc, connection))
                {
                    var id = new SqlParameter(EventIdParameter, eventId);
                    command.Parameters.Add(id);

                    command.CommandType = CommandType.StoredProcedure;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<EventDomain>> GetAllAsync()
        {
            // variable for return
            var events = new List<EventDomain>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectAllSqlExpression, connection))
                {
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        events.Add(new EventDomain
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField)),
                            LayoutId = dataReader.GetInt32(dataReader.GetOrdinal(LayoutIdField)),
                            Name = dataReader.GetString(dataReader.GetOrdinal(NameField)),
                            Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField)),
                            StartDateTime = dataReader.GetDateTime(dataReader.GetOrdinal(StartDateTimeField)),
                            FinishDateTime = dataReader.GetDateTime(dataReader.GetOrdinal(FinishDateTimeField)),
                        });
                    }
                }
            }

            return events;
        }

        public async Task<EventDomain> GetByIdAsync(int itemId)
        {
            // variable for return
            var eventDomain = new EventDomain();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectByIdSqlExpression, connection))
                {
                    var id = new SqlParameter(EventIdParameter, itemId);
                    command.Parameters.Add(id);
                    var dataReader = await command.ExecuteReaderAsync();
                    dataReader.Read();

                    eventDomain.Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField));
                    eventDomain.LayoutId = dataReader.GetInt32(dataReader.GetOrdinal(LayoutIdField));
                    eventDomain.Name = dataReader.GetString(dataReader.GetOrdinal(NameField));
                    eventDomain.Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField));
                    eventDomain.StartDateTime = dataReader.GetDateTime(dataReader.GetOrdinal(StartDateTimeField));
                    eventDomain.FinishDateTime = dataReader.GetDateTime(dataReader.GetOrdinal(FinishDateTimeField));
                }
            }

            return eventDomain;
        }

        public async Task<EventDomain> UpdateAsync(EventDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(UpdateSqlStoredProc, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(EventIdParameter, item?.Id),
                            new SqlParameter(NameParameter, item?.Name),
                            new SqlParameter(DescriptionParameter, item?.Description),
                            new SqlParameter(StartDateTimeParameter, item?.StartDateTime),
                            new SqlParameter(FinishDateTimeParameter, item?.FinishDateTime),
                        });

                    command.CommandType = CommandType.StoredProcedure;
                    await command.ExecuteNonQueryAsync();
                    var updatedItem = await GetByIdAsync((int)item?.Id);
                    return updatedItem;
                }
            }
        }
    }
}