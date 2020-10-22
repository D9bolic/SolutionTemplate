using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Repositories.Interfaces
{
    public interface IEventSeatRepository
    {
        /// <summary>
        /// Get eventSeat by id from database.
        /// </summary>
        /// <param name="eventSeatId">EventSeat id for get.</param>
        /// <returns>EventSeat by id from database.</returns>
        Task<EventSeatDomain> GetByIdAsync(int eventSeatId);

        /// <summary>
        /// Get all eventSeats from database.
        /// </summary>
        /// <returns>List of eventSeats.</returns>
        Task<List<EventSeatDomain>> GetAllAsync();

        /// <summary>
        /// Update eventSeat in database. Change only state.
        /// </summary>
        /// <param name="eventSeat">EventSeat for update.</param>
        /// <returns>Updated eventSeat.</returns>
        Task<EventSeatDomain> UpdateAsync(EventSeatDomain eventSeat);
    }
}
