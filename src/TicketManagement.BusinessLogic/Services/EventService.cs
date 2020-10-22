using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Extentions;
using Ticketmanagement.BusinessLogic.Mappers;
using Ticketmanagement.BusinessLogic.Services.Interfaces;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace Ticketmanagement.BusinessLogic.Services
{
    internal class EventService : BaseValidationService<EventDto>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly ILayoutRepository _layoutRepository;

        public EventService(
            IEventRepository eventRepository,
            IAreaRepository areaRepository,
            ISeatRepository seatRepository,
            ILayoutRepository layoutRepository)
        {
            _eventRepository = eventRepository;
            _areaRepository = areaRepository;
            _seatRepository = seatRepository;
            _layoutRepository = layoutRepository;
        }

        private static void ValidateIsPriceMoreThanZero(decimal price)
        {
            if (price < Constants.BottomLimitPrice)
            {
                throw new NegativePriceException();
            }
        }

        private static void ValidateDateTimeForPast(DateTime startDateTime)
        {
            if (startDateTime < DateTime.Now)
            {
                throw new UnsuitableDateTimeException("You can not create event in the past.");
            }
        }

        private static void ValidateIsFinishAfterStartDateTime(DateTime startDateTime, DateTime finishDateTime)
        {
            if (startDateTime > finishDateTime)
            {
                throw new UnsuitableDateTimeException("You can not create event with finish time before start time.");
            }
        }

        private async Task ValidateIsLayoutExistanceAsync(int layoutId)
        {
            var allLayouts = await _layoutRepository.GetAllAsync();

            if (!allLayouts.Any(layout => layout.Id == layoutId))
            {
                throw new ItemExistenceException("layouts have not got the id");
            }
        }

        private async Task ValidateIsItemExistanceAsync(int id)
        {
            var allEvents = await _eventRepository.GetAllAsync();

            if (!allEvents.Any(event_ => event_.Id == id))
            {
                throw new ItemExistenceException("events have not got the id");
            }
        }

        private async Task ValidateIsDescriptionUniqueAsync(string description, int id = 0)
        {
            var allEvents = await _eventRepository.GetAllAsync();

            var isNotUniqueDescription = allEvents
                .Where(ev => ev.Id != id)
                .Any(ev => ev.Description == description);

            if (isNotUniqueDescription)
            {
                throw new DescriptionUniqueException("event description is not unique");
            }
        }

        private async Task ValidateIsEventHasSeatsAsync(EventDto eventDto)
        {
            var areas = await _areaRepository.GetAllAsync();
            var seats = await _seatRepository.GetAllAsync();

            var hasTheEventAnySeat = (from area in areas
                                      join seat in seats on area.Id equals seat.AreaId
                                      where area.LayoutId == eventDto.LayoutId
                                      select seat.Id)
                                      .Any();

            if (!hasTheEventAnySeat)
            {
                throw new EventHasNotSeatException();
            }
        }

        private async Task ValidateIsEventHasFreePlaceAsync(EventDto eventDto)
        {
            var events = await _eventRepository.GetAllAsync();
            var layouts = await _layoutRepository.GetAllAsync();

            // get current venueId
            var currentLayout = await _layoutRepository.GetByIdAsync(eventDto.LayoutId);
            var venueId = currentLayout.VenueId;

            var allEventsForCurrentVenue = from ev in events
                                           join layout in layouts on ev.LayoutId equals layout.Id
                                           where layout.VenueId == venueId
                                           select ev;

            // when st2 < st1 < f2
            // when st2 < f1 < f2
            // when st1 < st2 < f2 < f1
            var isLayoutBusyForThisTime = allEventsForCurrentVenue
                .Any(ev => eventDto.StartDateTime.IsBetween(ev.StartDateTime, ev.FinishDateTime)
                || eventDto.FinishDateTime.IsBetween(ev.StartDateTime, ev.FinishDateTime)
                || (eventDto.StartDateTime < ev.StartDateTime && ev.FinishDateTime < eventDto.FinishDateTime));

            if (isLayoutBusyForThisTime)
            {
                throw new UnsuitableDateTimeException();
            }
        }

        public async Task<EventDto> CreateAsync(EventDto item, decimal price)
        {
            ValidateItemForNull(item);
            ValidateModel(item);

            ValidateIsPriceMoreThanZero(price);
            ValidateDateTimeForPast(item.StartDateTime);
            ValidateIsFinishAfterStartDateTime(item.StartDateTime, item.FinishDateTime);

            await ValidateIsLayoutExistanceAsync(item.LayoutId);
            await ValidateIsDescriptionUniqueAsync(item.Description);
            await ValidateIsEventHasSeatsAsync(item);
            await ValidateIsEventHasFreePlaceAsync(item);

            var eventDomain = EventMapper.GetEventDomain(item);

            var insertedItem = await _eventRepository.CreateAsync(eventDomain, price);
            return EventMapper.GetEventDto(insertedItem);
        }

        public async Task DeleteAsync(int eventId)
        {
            ValidateIsIdMoreThanZero(eventId);
            await ValidateIsItemExistanceAsync(eventId);

            await _eventRepository.DeleteAsync(eventId);
        }

        public async Task<IQueryable<EventDto>> GetAllAsync()
        {
            var domainList = await _eventRepository.GetAllAsync();

            var dtoList = domainList
                .Select(ev => EventMapper.GetEventDto(ev))
                .AsQueryable<EventDto>();

            return dtoList;
        }

        public async Task<EventDto> GetByIdAsync(int eventId)
        {
            ValidateIsIdMoreThanZero(eventId);
            await ValidateIsItemExistanceAsync(eventId);

            var eventDomain = await _eventRepository.GetByIdAsync(eventId);
            var eventDto = EventMapper.GetEventDto(eventDomain);

            return eventDto;
        }

        public async Task<EventDto> UpdateAsync(EventDto item)
        {
            ValidateItemForNull(item);
            ValidateModel(item);

            ValidateDateTimeForPast(item.StartDateTime);
            ValidateIsFinishAfterStartDateTime(item.StartDateTime, item.FinishDateTime);
            ValidateIsIdMoreThanZero(item.Id);

            await ValidateIsItemExistanceAsync(item.Id);
            await ValidateIsDescriptionUniqueAsync(item.Description, item.Id);
            await ValidateIsEventHasFreePlaceAsync(item);

            var venueDomain = EventMapper.GetEventDomain(item);

            var updatedItem = await _eventRepository.UpdateAsync(venueDomain);
            return EventMapper.GetEventDto(updatedItem);
        }
    }
}