using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Enums;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventSeatRepository : IEventSeatRepository
    {
        private const string IdField = "Id";
        private const string EventAreaIdField = "EventAreaId";
        private const string RowField = "Row";
        private const string NumberField = "Number";
        private const string StateField = "State";

        private const string IdParameter = "@id";
        private const string StateParameter = "@state";

        private const string SelectAllSqlExpression = @"select Id, EventAreaId, Row, Number, State 
                                                        from EventSeat";

        private const string SelectByIdSqlExpression = @"select Id, EventAreaId, Row, Number, State 
                                                         from EventSeat 
                                                         where Id = @id";

        private const string UdateSqlExpression = @"update EventSeat 
                                                    set State = @state 
                                                    where Id = @id";

        private readonly string _connectionString;

        public EventSeatRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<EventSeatDomain>> GetAllAsync()
        {
            // variable for return
            var eventSeats = new List<EventSeatDomain>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectAllSqlExpression, connection))
                {
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        eventSeats.Add(new EventSeatDomain
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField)),
                            EventAreaId = dataReader.GetInt32(dataReader.GetOrdinal(EventAreaIdField)),
                            Row = dataReader.GetInt32(dataReader.GetOrdinal(RowField)),
                            Number = dataReader.GetInt32(dataReader.GetOrdinal(NumberField)),
                            State = (EventSeatState)dataReader.GetInt32(dataReader.GetOrdinal(StateField)),
                        });
                    }
                }
            }

            return eventSeats;
        }

        public async Task<EventSeatDomain> GetByIdAsync(int eventSeatId)
        {
            // variable for return
            var eventSeat = new EventSeatDomain();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectByIdSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, eventSeatId);
                    command.Parameters.Add(id);
                    var dataReader = await command.ExecuteReaderAsync();
                    dataReader.Read();

                    eventSeat.Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField));
                    eventSeat.EventAreaId = dataReader.GetInt32(dataReader.GetOrdinal(EventAreaIdField));
                    eventSeat.Row = dataReader.GetInt32(dataReader.GetOrdinal(RowField));
                    eventSeat.Number = dataReader.GetInt32(dataReader.GetOrdinal(NumberField));
                    eventSeat.State = (EventSeatState)dataReader.GetInt32(dataReader.GetOrdinal(StateField));
                }
            }

            return eventSeat;
        }

        public async Task<EventSeatDomain> UpdateAsync(EventSeatDomain eventSeat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(UdateSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(StateParameter, eventSeat?.State),
                            new SqlParameter(IdParameter, eventSeat?.Id),
                        });

                    await command.ExecuteNonQueryAsync();
                    var updatedItem = await GetByIdAsync((int)eventSeat?.Id);
                    return updatedItem;
                }
            }
        }
    }
}