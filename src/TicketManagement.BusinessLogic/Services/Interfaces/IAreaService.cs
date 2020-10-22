using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.DTO;

namespace Ticketmanagement.BusinessLogic.Services.Interfaces
{
    internal interface IAreaService
    {
        /// <summary>
        /// Get area by id after validation.
        /// </summary>
        /// <param name="areaId">Area id for get.</param>
        /// <returns>Area by id.</returns>
        Task<AreaDto> GetByIdAsync(int areaId);

        /// <summary>
        /// Get all areas from database.
        /// </summary>
        /// <returns>List of areas.</returns>
        Task<IQueryable<AreaDto>> GetAllAsync();

        /// <summary>
        /// Create area after validation.
        /// </summary>
        /// <param name="item">Area for create.</param>
        /// <returns>Created area.</returns>
        Task<AreaDto> CreateAsync(AreaDto item);

        /// <summary>
        /// Update area after validation.
        /// </summary>
        /// <param name="item">Area for update.</param>
        /// <returns>Updated area.</returns>
        Task<AreaDto> UpdateAsync(AreaDto item);

        /// <summary>
        /// Delete area by id after validation.
        /// </summary>
        /// <param name="areaId">Area id for delete.</param>
        Task DeleteAsync(int areaId);
    }
}
