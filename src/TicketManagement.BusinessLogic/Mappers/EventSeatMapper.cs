using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Models;

namespace Ticketmanagement.BusinessLogic.Mappers
{
    internal static class EventSeatMapper
    {
        /// <summary>
        /// Get eventSeat domain model with the same fields.
        /// </summary>
        public static EventSeatDomain GetEventSeatDomain(EventSeatDto eventSeatDto)
        {
            var eventSeatDomain = new EventSeatDomain
            {
                Id = eventSeatDto.Id,
                EventAreaId = eventSeatDto.EventAreaId,
                Row = eventSeatDto.Row,
                Number = eventSeatDto.Number,
                State = eventSeatDto.State,
            };

            return eventSeatDomain;
        }

        /// <summary>
        /// Get eventSeat dto model with the same fields.
        /// </summary>
        public static EventSeatDto GetEventSeatDto(EventSeatDomain eventSeatDomain)
        {
            var eventSeatDto = new EventSeatDto
            {
                Id = eventSeatDomain.Id,
                EventAreaId = eventSeatDomain.EventAreaId,
                Row = eventSeatDomain.Row,
                Number = eventSeatDomain.Number,
                State = eventSeatDomain.State,
            };

            return eventSeatDto;
        }
    }
}
