using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.DTO;

namespace Ticketmanagement.BusinessLogic.Services.Interfaces
{
    internal interface IEventService
    {
        /// <summary>
        /// Get event by id after validation.
        /// </summary>
        /// <param name="eventId">Event id for get.</param>
        /// <returns>Event by id.</returns>
        Task<EventDto> GetByIdAsync(int eventId);

        /// <summary>
        /// Get all events from database.
        /// </summary>
        /// <returns>List of events.</returns>
        Task<IQueryable<EventDto>> GetAllAsync();

        /// <summary>
        /// Create event with eventAreas and eventSeats after validation.
        /// </summary>
        /// <param name="item">Event for create.</param>
        /// <param name="price">Price by default in eventArea.</param>
        /// <returns>Created event.</returns>
        Task<EventDto> CreateAsync(EventDto item, decimal price);

        /// <summary>
        /// Update event after validation. Update without change layout.
        /// </summary>
        /// <param name="item">Event for update.</param>
        /// <returns>Updated event.</returns>
        Task<EventDto> UpdateAsync(EventDto item);

        /// <summary>
        /// Delete evennt by id after validation.
        /// </summary>
        /// <param name="eventId">Event id for delete.</param>
        Task DeleteAsync(int eventId);
    }
}
