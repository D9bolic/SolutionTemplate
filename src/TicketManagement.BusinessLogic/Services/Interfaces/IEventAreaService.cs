using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.DTO;

namespace Ticketmanagement.BusinessLogic.Services.Interfaces
{
    internal interface IEventAreaService
    {
        /// <summary>
        /// Get eventArea by id after validation.
        /// </summary>
        /// <param name="eventAreaId">EventArea id for get.</param>
        /// <returns>EvetArea by id.</returns>
        Task<EventAreaDto> GetByIdAsync(int eventAreaId);

        /// <summary>
        /// Get all eventAreas from database.
        /// </summary>
        /// <returns>List of eventAreas.</returns>
        Task<IQueryable<EventAreaDto>> GetAllAsync();

        /// <summary>
        /// Update eventArea after validation. Update without change event and coords.
        /// </summary>
        /// <param name="eventAreaDto">EventArea for update.</param>
        /// <returns>Updated eventArea.</returns>
        Task<EventAreaDto> UpdateAsync(EventAreaDto eventAreaDto);
    }
}
