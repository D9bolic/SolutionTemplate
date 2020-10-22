using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        /// <summary>
        /// Get seat from database by id.
        /// </summary>
        /// <param name="itemId">Seat id for get.</param>
        /// <returns>Seat by id from database.</returns>
        Task<SeatDomain> GetByIdAsync(int itemId);

        /// <summary>
        /// Get all seats from database.
        /// </summary>
        /// <returns>List of seats.</returns>
        Task<List<SeatDomain>> GetAllAsync();

        /// <summary>
        /// Create new seat in database.
        /// </summary>
        /// <param name="item">Seat for create.</param>
        /// <returns>Created seat.</returns>
        Task<SeatDomain> CreateAsync(SeatDomain item);

        /// <summary>
        /// Update seat in database.
        /// </summary>
        /// <param name="item">Seat for update.</param>
        /// <returns>Updated seat.</returns>
        Task<SeatDomain> UpdateAsync(SeatDomain item);

        /// <summary>
        /// Delet seat from database by id.
        /// </summary>
        /// <param name="itemId">Seat id for delete.</param>
        Task DeleteAsync(int itemId);
    }
}
