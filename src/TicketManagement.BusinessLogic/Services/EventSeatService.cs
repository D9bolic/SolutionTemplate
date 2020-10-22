using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using Ticketmanagement.BusinessLogic.Services.Interfaces;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace Ticketmanagement.BusinessLogic.Services
{
    internal class EventSeatService : BaseValidationService<EventSeatDto>, IEventSeatService
    {
        private readonly IEventSeatRepository _eventSeatRepository;

        public EventSeatService(IEventSeatRepository eventSeatRepository)
        {
            _eventSeatRepository = eventSeatRepository;
        }

        private async Task ValidateIsItemExistanceAsync(int id)
        {
            var allEventSeats = await _eventSeatRepository.GetAllAsync();

            if (!allEventSeats.Any(eventSeat => eventSeat.Id == id))
            {
                throw new ItemExistenceException($"eventSeats have not got the id = {id}");
            }
        }

        public async Task<IQueryable<EventSeatDto>> GetAllAsync()
        {
            var domainList = await _eventSeatRepository.GetAllAsync();

            var dtoList = domainList
                .Select(eventSeat => EventSeatMapper.GetEventSeatDto(eventSeat))
                .AsQueryable<EventSeatDto>();

            return dtoList;
        }

        public async Task<EventSeatDto> GetByIdAsync(int eventSeatId)
        {
            ValidateIsIdMoreThanZero(eventSeatId);
            await ValidateIsItemExistanceAsync(eventSeatId);

            var eventSeatDomain = await _eventSeatRepository.GetByIdAsync(eventSeatId);
            var eventSeatDto = EventSeatMapper.GetEventSeatDto(eventSeatDomain);

            return eventSeatDto;
        }

        public async Task<EventSeatDto> UpdateAsync(EventSeatDto eventSeat)
        {
            ValidateItemForNull(eventSeat);
            ValidateIsIdMoreThanZero(eventSeat.Id);
            await ValidateIsItemExistanceAsync(eventSeat.Id);

            var eventSeatDomain = EventSeatMapper.GetEventSeatDomain(eventSeat);

            var updatedItem = await _eventSeatRepository.UpdateAsync(eventSeatDomain);
            return EventSeatMapper.GetEventSeatDto(updatedItem);
        }
    }
}