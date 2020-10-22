using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.DTO;

namespace Ticketmanagement.BusinessLogic.Services.Interfaces
{
    internal interface ISeatService
    {
        /// <summary>
        /// Get seat by id after validation.
        /// </summary>
        /// <param name="seatId">Seat id for get.</param>
        /// <returns>Seat by id.</returns>
        Task<SeatDto> GetByIdAsync(int seatId);

        /// <summary>
        /// Get all seats from databse.
        /// </summary>
        /// <returns>List of seats.</returns>
        Task<IQueryable<SeatDto>> GetAllAsync();

        /// <summary>
        /// Create seat after validation.
        /// </summary>
        /// <param name="item">Seat for create.</param>
        /// <returns>Created seat.</returns>
        Task<SeatDto> CreateAsync(SeatDto item);

        /// <summary>
        /// Update seat after validation.
        /// </summary>
        /// <param name="item">Seat for update.</param>
        /// <returns>Updated seat.</returns>
        Task<SeatDto> UpdateAsync(SeatDto item);

        /// <summary>
        /// Delete seat after vaidation.
        /// </summary>
        /// <param name="seatId">Seat id for delete.</param>
        Task DeleteAsync(int seatId);
    }
}
