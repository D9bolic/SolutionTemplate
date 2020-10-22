using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.DTO;

namespace Ticketmanagement.BusinessLogic.Services.Interfaces
{
    internal interface IVenueService
    {
        /// <summary>
        /// Get venue by id after validation.
        /// </summary>
        /// <param name="venueId">Venue id for get.</param>
        /// <returns>Venue by id.</returns>
        Task<VenueDto> GetByIdAsync(int venueId);

        /// <summary>
        /// Get all venues from database.
        /// </summary>
        /// <returns>List of venues.</returns>
        Task<IQueryable<VenueDto>> GetAllAsync();

        /// <summary>
        /// Create new venue after validation.
        /// </summary>
        /// <param name="item">Venue for create.</param>
        /// <returns>Created venue.</returns>
        Task<VenueDto> CreateAsync(VenueDto item);

        /// <summary>
        /// Update venue after validation.
        /// </summary>
        /// <param name="item">Venue for update.</param>
        /// <returns>Updated venue.</returns>
        Task<VenueDto> UpdateAsync(VenueDto item);

        /// <summary>
        /// Delete venue by id after validation.
        /// </summary>
        /// <param name="venueId">Venue id for delete.</param>
        Task DeleteAsync(int venueId);
    }
}
