using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Repositories.Interfaces
{
    public interface IEventAreaRepository
    {
        /// <summary>
        /// Get eventArea by id from database.
        /// </summary>
        /// <param name="eventAreaId">Id of eventArea for get.</param>
        /// <returns>EventArea by id from database.</returns>
        Task<EventAreaDomain> GetByIdAsync(int eventAreaId);

        /// <summary>
        /// Get all eventAreas from database.
        /// </summary>
        /// <returns>List of eventAreas.</returns>
        Task<List<EventAreaDomain>> GetAllAsync();

        /// <summary>
        /// Update eventArea in the database. Update without change event and coords.
        /// </summary>
        /// <param name="eventArea">EventArea for update.</param>
        /// <returns>Updated eventArea.</returns>
        Task<EventAreaDomain> UpdateAsync(EventAreaDomain eventArea);
    }
}
