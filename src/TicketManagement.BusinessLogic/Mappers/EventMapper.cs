using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Models;

namespace Ticketmanagement.BusinessLogic.Mappers
{
    internal static class EventMapper
    {
        /// <summary>
        /// Get event domain model with the same fields.
        /// </summary>
        public static EventDomain GetEventDomain(EventDto eventDto)
        {
            var eventDomain = new EventDomain
            {
                Id = eventDto.Id,
                LayoutId = eventDto.LayoutId,
                Name = eventDto.Name,
                Description = eventDto.Description,
                StartDateTime = eventDto.StartDateTime,
                FinishDateTime = eventDto.FinishDateTime,
            };

            return eventDomain;
        }

        /// <summary>
        /// Get event dto model with the same fields.
        /// </summary>
        public static EventDto GetEventDto(EventDomain eventDomain)
        {
            var eventDto = new EventDto
            {
                Id = eventDomain.Id,
                LayoutId = eventDomain.LayoutId,
                Name = eventDomain.Name,
                Description = eventDomain.Description,
                StartDateTime = eventDomain.StartDateTime,
                FinishDateTime = eventDomain.FinishDateTime,
            };

            return eventDto;
        }
    }
}
