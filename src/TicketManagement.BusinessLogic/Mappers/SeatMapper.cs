using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Models;

namespace Ticketmanagement.BusinessLogic.Mappers
{
    internal static class SeatMapper
    {
        /// <summary>
        /// Get seat domain model with the same fields.
        /// </summary>
        public static SeatDomain GetSeatDomain(SeatDto seatDto)
        {
            var seatDomain = new SeatDomain
            {
                Id = seatDto.Id,
                AreaId = seatDto.AreaId,
                Row = seatDto.Row,
                Number = seatDto.Number,
            };

            return seatDomain;
        }

        /// <summary>
        /// Get seat dto model with the same fields.
        /// </summary>
        public static SeatDto GetSeatDto(SeatDomain seatDomain)
        {
            var seatDto = new SeatDto
            {
                Id = seatDomain.Id,
                AreaId = seatDomain.AreaId,
                Row = seatDomain.Row,
                Number = seatDomain.Number,
            };

            return seatDto;
        }
    }
}
