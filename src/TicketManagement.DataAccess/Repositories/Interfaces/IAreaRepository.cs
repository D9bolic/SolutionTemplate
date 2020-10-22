using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Repositories.Interfaces
{
    public interface IAreaRepository
    {
        /// <summary>
        /// Get area by id from database.
        /// </summary>
        /// <param name="itemId">Id of area.</param>
        /// <returns>Area by id from database.</returns>
        Task<AreaDomain> GetByIdAsync(int itemId);

        /// <summary>
        /// Get all areas from database.
        /// </summary>
        /// <returns>List of the areas.</returns>
        Task<List<AreaDomain>> GetAllAsync();

        /// <summary>
        /// Create new area and returnt one.
        /// </summary>
        /// <param name="item">Area for create.</param>
        /// <returns>Area with updated data from database.</returns>
        Task<AreaDomain> CreateAsync(AreaDomain item);

        /// <summary>
        /// Update area in the database.
        /// </summary>
        /// <param name="item">Area for update.</param>
        /// <returns>Updated area.</returns>
        Task<AreaDomain> UpdateAsync(AreaDomain item);

        /// <summary>
        /// Delete Area from database.
        /// </summary>
        /// <param name="itemId">Area id for delete the item.</param>
        Task DeleteAsync(int itemId);
    }
}
