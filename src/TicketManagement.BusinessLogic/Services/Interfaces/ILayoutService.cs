using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.DTO;

namespace Ticketmanagement.BusinessLogic.Services.Interfaces
{
    internal interface ILayoutService
    {
        /// <summary>
        /// Get by id after validation.
        /// </summary>
        /// <param name="layoutId">Layout id for get.</param>
        /// <returns>Layout by id.</returns>
        Task<LayoutDto> GetByIdAsync(int layoutId);

        /// <summary>
        /// Get all layouts from database.
        /// </summary>
        /// <returns>List of layouts.</returns>
        Task<IQueryable<LayoutDto>> GetAllAsync();

        /// <summary>
        /// Create layout after validation.
        /// </summary>
        /// <param name="item">Layout for create.</param>
        /// <returns>Created layout.</returns>
        Task<LayoutDto> CreateAsync(LayoutDto item);

        /// <summary>
        /// Update layout after validation.
        /// </summary>
        /// <param name="item">Layout for update.</param>
        /// <returns>Updated layout.</returns>
        Task<LayoutDto> UpdateAsync(LayoutDto item);

        /// <summary>
        /// Delete layout by id after validation.
        /// </summary>
        /// <param name="layoutId">Layout id for delete.</param>
        Task DeleteAsync(int layoutId);
    }
}
