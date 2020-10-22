using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Models;

namespace Ticketmanagement.BusinessLogic.Mappers
{
    internal static class EventAreaMapper
    {
        /// <summary>
        /// Get eventArea domain model with the same fields.
        /// </summary>
        public static EventAreaDomain GetEventAreaDomain(EventAreaDto eventAreaDto)
        {
            var eventAreaDomain = new EventAreaDomain
            {
                Id = eventAreaDto.Id,
                EventId = eventAreaDto.EventId,
                Description = eventAreaDto.Description,
                CoordX = eventAreaDto.CoordX,
                CoordY = eventAreaDto.CoordY,
                Price = eventAreaDto.Price,
            };

            return eventAreaDomain;
        }

        /// <summary>
        /// Get eventArea dto model with the same fields.
        /// </summary>
        public static EventAreaDto GetEventAreaDto(EventAreaDomain eventAreaDomain)
        {
            var eventAreaDto = new EventAreaDto
            {
                Id = eventAreaDomain.Id,
                EventId = eventAreaDomain.EventId,
                Description = eventAreaDomain.Description,
                CoordX = eventAreaDomain.CoordX,
                CoordY = eventAreaDomain.CoordY,
                Price = eventAreaDomain.Price,
            };

            return eventAreaDto;
        }
    }
}
