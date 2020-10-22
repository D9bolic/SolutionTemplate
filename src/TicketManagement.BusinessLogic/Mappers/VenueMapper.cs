using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Models;

namespace Ticketmanagement.BusinessLogic.Mappers
{
    internal static class VenueMapper
    {
        /// <summary>
        /// Get venue domain model with the same fields.
        /// </summary>
        public static VenueDomain GetVenueDomain(VenueDto venueDto)
        {
            var venueDomain = new VenueDomain
            {
                Id = venueDto.Id,
                Description = venueDto.Description,
                Address = venueDto.Address,
                Phone = venueDto.Phone,
            };

            return venueDomain;
        }

        /// <summary>
        /// Get venue dto model with the same fields.
        /// </summary>
        public static VenueDto GetVenueDto(VenueDomain venueDomain)
        {
            var venueDto = new VenueDto
            {
                Id = venueDomain.Id,
                Description = venueDomain.Description,
                Address = venueDomain.Address,
                Phone = venueDomain.Phone,
            };

            return venueDto;
        }
    }
}
