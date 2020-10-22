using System;
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
    internal class EventAreaService : BaseValidationService<EventAreaDto>, IEventAreaService
    {
        private readonly IEventAreaRepository _eventAreaRepository;

        public EventAreaService(IEventAreaRepository eventAreaRepository)
        {
            _eventAreaRepository = eventAreaRepository;
        }

        private static void ValidateIsPriceMoreThanZero(decimal price)
        {
            if (price < Constants.BottomLimitPrice)
            {
                throw new NegativePriceException();
            }
        }

        private async Task ValidateIsItemExistanceAsync(int id)
        {
            var allEventAreas = await _eventAreaRepository.GetAllAsync();

            if (!allEventAreas.Any(eventArea => eventArea.Id == id))
            {
                throw new ItemExistenceException("eventAreas have not got the id");
            }
        }

        private async Task ValidateIsDescriptionUniqueAsync(string description, int eventId, int id = 0)
        {
            var allEventAreas = await _eventAreaRepository.GetAllAsync();

            var isNotUniqueEventAreaDescriptionForTheEvent = allEventAreas
                .Where(eventArea => eventArea.EventId == eventId && eventArea.Id != id)
                .Any(eventArea => eventArea.Description == description);

            if (isNotUniqueEventAreaDescriptionForTheEvent)
            {
                throw new DescriptionUniqueException("eventArea description is not unique for event");
            }
        }

        public async Task<IQueryable<EventAreaDto>> GetAllAsync()
        {
            var domainList = await _eventAreaRepository.GetAllAsync();

            var dtoList = domainList
                .Select(eventArea => EventAreaMapper.GetEventAreaDto(eventArea))
                .AsQueryable<EventAreaDto>();

            return dtoList;
        }

        public async Task<EventAreaDto> GetByIdAsync(int eventAreaId)
        {
            ValidateIsIdMoreThanZero(eventAreaId);
            await ValidateIsItemExistanceAsync(eventAreaId);

            var eventAreaDomain = await _eventAreaRepository.GetByIdAsync(eventAreaId);
            var eventAreaDto = EventAreaMapper.GetEventAreaDto(eventAreaDomain);

            return eventAreaDto;
        }

        public async Task<EventAreaDto> UpdateAsync(EventAreaDto eventAreaDto)
        {
            ValidateItemForNull(eventAreaDto);
            ValidateModel(eventAreaDto);
            ValidateIsPriceMoreThanZero(eventAreaDto.Price);
            ValidateIsIdMoreThanZero(eventAreaDto.Id);

            await ValidateIsDescriptionUniqueAsync(eventAreaDto.Description, eventAreaDto.EventId, eventAreaDto.Id);
            await ValidateIsItemExistanceAsync(eventAreaDto.Id);

            var eventAreaDomain = EventAreaMapper.GetEventAreaDomain(eventAreaDto);

            var updatedItem = await _eventAreaRepository.UpdateAsync(eventAreaDomain);
            return EventAreaMapper.GetEventAreaDto(updatedItem);
        }
    }
}