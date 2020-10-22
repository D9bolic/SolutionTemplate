using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Repositories.Interfaces
{
    public interface ILayoutRepository
    {
        /// <summary>
        /// Get layout by id from database.
        /// </summary>
        /// <param name="itemId">Layout id for get.</param>
        /// <returns>Layout by id from database.</returns>
        Task<LayoutDomain> GetByIdAsync(int itemId);

        /// <summary>
        /// Get all layouts from database.
        /// </summary>
        /// <returns>List of layouts.</returns>
        Task<List<LayoutDomain>> GetAllAsync();

        /// <summary>
        /// Create new layout in database.
        /// </summary>
        /// <param name="item">Layout for create.</param>
        /// <returns>Created layout.</returns>
        Task<LayoutDomain> CreateAsync(LayoutDomain item);

        /// <summary>
        /// Update layout in database.
        /// </summary>
        /// <param name="item">Layout for update.</param>
        /// <returns>Updated lauoyt.</returns>
        Task<LayoutDomain> UpdateAsync(LayoutDomain item);

        /// <summary>
        /// Delete layout from the database by id.
        /// </summary>
        /// <param name="itemId">Layout id for delete.</param>
        Task DeleteAsync(int itemId);
    }
}
