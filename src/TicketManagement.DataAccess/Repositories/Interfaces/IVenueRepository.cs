using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Repositories.Interfaces
{
    public interface IVenueRepository
    {
        /// <summary>
        /// Get venue by id from database.
        /// </summary>
        /// <param name="itemId">Venue id for get.</param>
        /// <returns>Venue by id from database.</returns>
        Task<VenueDomain> GetByIdAsync(int itemId);

        /// <summary>
        /// Get all venues from database.
        /// </summary>
        /// <returns>List of venues.</returns>
        Task<List<VenueDomain>> GetAllAsync();

        /// <summary>
        /// Create new venue in database.
        /// </summary>
        /// <param name="item">Venue for create.</param>
        /// <returns>Created venue.</returns>
        Task<VenueDomain> CreateAsync(VenueDomain item);

        /// <summary>
        /// Update venue in database.
        /// </summary>
        /// <param name="item">Venue for update.</param>
        /// <returns>Updated venue.</returns>
        Task<VenueDomain> UpdateAsync(VenueDomain item);

        /// <summary>
        /// Delete venue by id from database.
        /// </summary>
        /// <param name="itemId">Venue id for delete.</param>
        Task DeleteAsync(int itemId);
    }
}
