using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.DTO;

namespace Ticketmanagement.BusinessLogic.Services.Interfaces
{
    internal interface IEventSeatService
    {
        /// <summary>
        /// Get eventSeat by id after validation.
        /// </summary>
        /// <param name="eventSeatId">EventSeat id for get.</param>
        /// <returns>EventSeat by id.</returns>
        Task<EventSeatDto> GetByIdAsync(int eventSeatId);

        /// <summary>
        /// Get all eventSeat from database.
        /// </summary>
        /// <returns>List of eventSeats.</returns>
        Task<IQueryable<EventSeatDto>> GetAllAsync();

        /// <summary>
        /// Update eventSeat after validation. Change only state.
        /// </summary>
        /// <param name="eventSeat">EventSeat for update.</param>
        /// <returns>Updated event seat.</returns>
        Task<EventSeatDto> UpdateAsync(EventSeatDto eventSeat);
    }
}
