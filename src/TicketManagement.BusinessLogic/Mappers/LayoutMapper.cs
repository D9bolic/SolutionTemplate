using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Models;

namespace Ticketmanagement.BusinessLogic.Mappers
{
    internal static class LayoutMapper
    {
        /// <summary>
        /// Get layout domain model with the same fields.
        /// </summary>
        public static LayoutDomain GetLayoutDomain(LayoutDto layoutDto)
        {
            var layoutDomain = new LayoutDomain
            {
                Id = layoutDto.Id,
                VenueId = layoutDto.VenueId,
                Description = layoutDto.Description,
            };

            return layoutDomain;
        }

        /// <summary>
        /// Get layout dto model with the same fields.
        /// </summary>
        public static LayoutDto GetLayoutDto(LayoutDomain layoutDomain)
        {
            var layoutDto = new LayoutDto
            {
                Id = layoutDomain.Id,
                VenueId = layoutDomain.VenueId,
                Description = layoutDomain.Description,
            };

            return layoutDto;
        }
    }
}
