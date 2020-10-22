using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Repositories.Interfaces
{
    public interface IEventRepository
    {
        /// <summary>
        /// Get event by id from database.
        /// </summary>
        /// <param name="itemId">Event id for get from database.</param>
        /// <returns>Event by id from database.</returns>
        Task<EventDomain> GetByIdAsync(int itemId);

        /// <summary>
        /// Get all events from database.
        /// </summary>
        /// <returns>List of events.</returns>
        Task<List<EventDomain>> GetAllAsync();

        /// <summary>
        /// Create new event in the database with eventAreas and eventSeats. EventAreas will have default price equals the price as parameter.
        /// EventSeats will be free by default.
        /// </summary>
        /// <param name="item">Event for create.</param>
        /// <param name="price">Default price for eventAreas.</param>
        /// <returns>Created event.</returns>
        Task<EventDomain> CreateAsync(EventDomain item, decimal price);

        /// <summary>
        /// Update event in database. Update without change layout.
        /// </summary>
        /// <param name="item">Event for update.</param>
        /// <returns>Updated event.</returns>
        Task<EventDomain> UpdateAsync(EventDomain item);

        /// <summary>
        /// Delete event in database.
        /// </summary>
        /// <param name="eventId">Event id for delete.</param>
        Task DeleteAsync(int eventId);
    }
}
