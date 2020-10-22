using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class SeatRepository : ISeatRepository
    {
        private const string IdField = "Id";
        private const string AreaIdField = "AreaId";
        private const string RowField = "Row";
        private const string NumberField = "Number";

        private const string IdParameter = "@id";
        private const string AreaIdParameter = "@areaId";
        private const string RowParameter = "@row";
        private const string NumberParameter = "@number";

        private const string DeleteSqlExpression = @"delete from Seat 
                                                     where Id = @id";

        private const string SelectByIdSqlExpression = @"select Id, AreaId, Row, Number 
                                                         from Seat 
                                                         where Id = @id";

        private const string SelectAllSqlExpression = @"select Id, AreaId, Row, Number 
                                                        from Seat";

        private const string InsertSqlExpression = @"insert into Seat (AreaId, Row, Number) 
                                                     output inserted.Id 
                                                     values (@areaId, @row, @number)";

        private const string UpdateSqlExpression = @"update Seat 
                                                     set AreaId = @areaId, Row = @row, Number = @number 
                                                     where Id = @id";

        private readonly string _connectionString;

        public SeatRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SeatDomain> CreateAsync(SeatDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(InsertSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(AreaIdParameter, item?.AreaId),
                            new SqlParameter(RowParameter, item?.Row),
                            new SqlParameter(NumberParameter, item?.Number),
                        });

                    var addedItemId = await command.ExecuteScalarAsync();
                    var addedItem = await GetByIdAsync((int)addedItemId);
                    return addedItem;
                }
            }
        }

        public async Task DeleteAsync(int itemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(DeleteSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, itemId);
                    command.Parameters.Add(id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<SeatDomain>> GetAllAsync()
        {
            // variable for return
            var seats = new List<SeatDomain>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectAllSqlExpression, connection))
                {
                    var dataReader = await command.ExecuteReaderAsync();

                    while (dataReader.Read())
                    {
                        seats.Add(new SeatDomain
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField)),
                            AreaId = dataReader.GetInt32(dataReader.GetOrdinal(AreaIdField)),
                            Row = dataReader.GetInt32(dataReader.GetOrdinal(RowField)),
                            Number = dataReader.GetInt32(dataReader.GetOrdinal(NumberField)),
                        });
                    }
                }
            }

            return seats;
        }

        public async Task<SeatDomain> GetByIdAsync(int itemId)
        {
            // variable for return
            var seat = new SeatDomain();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectByIdSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, itemId);
                    command.Parameters.Add(id);
                    var dataReader = await command.ExecuteReaderAsync();
                    dataReader.Read();

                    seat.Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField));
                    seat.AreaId = dataReader.GetInt32(dataReader.GetOrdinal(AreaIdField));
                    seat.Row = dataReader.GetInt32(dataReader.GetOrdinal(RowField));
                    seat.Number = dataReader.GetInt32(dataReader.GetOrdinal(NumberField));
                }
            }

            return seat;
        }

        public async Task<SeatDomain> UpdateAsync(SeatDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(UpdateSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(AreaIdParameter, item?.AreaId),
                            new SqlParameter(RowParameter, item?.Row),
                            new SqlParameter(NumberParameter, item?.Number),
                            new SqlParameter(IdParameter, item?.Id),
                        });

                    await command.ExecuteNonQueryAsync();
                    var updatedItem = await GetByIdAsync((int)item?.Id);
                    return updatedItem;
                }
            }
        }
    }
}