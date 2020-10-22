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
    internal class LayoutService : BaseValidationService<LayoutDto>, ILayoutService
    {
        private readonly ILayoutRepository _layoutRepository;
        private readonly IVenueRepository _venueRepository;

        public LayoutService(ILayoutRepository layoutRepository, IVenueRepository venueRepository)
        {
            _layoutRepository = layoutRepository;
            _venueRepository = venueRepository;
        }

        private async Task ValidateIsVenueExistanceAsync(int venueId)
        {
            var allVenues = await _venueRepository.GetAllAsync();

            if (!allVenues.Any(venue => venue.Id == venueId))
            {
                throw new ItemExistenceException("venues have not got the id");
            }
        }

        private async Task ValidateIsItemExistanceAsync(int id)
        {
            var allLayouts = await _layoutRepository.GetAllAsync();

            if (!allLayouts.Any(layout => layout.Id == id))
            {
                throw new ItemExistenceException("layouts have not got the id");
            }
        }

        private async Task ValidateIsDescriptionUniqueAsync(string description, int venueId, int id = 0)
        {
            var layouts = await _layoutRepository.GetAllAsync();

            var isNotUniqueDescritionForTheVenue = layouts
                .Where(layout => layout.VenueId == venueId && layout.Id != id)
                .Any(layout => layout.Description == description);

            if (isNotUniqueDescritionForTheVenue)
            {
                throw new DescriptionUniqueException("layouts description is not unique for venue");
            }
        }

        public async Task<LayoutDto> CreateAsync(LayoutDto item)
        {
            ValidateItemForNull(item);
            ValidateModel(item);

            await ValidateIsDescriptionUniqueAsync(item.Description, item.VenueId);
            await ValidateIsVenueExistanceAsync(item.VenueId);

            var layout = LayoutMapper.GetLayoutDomain(item);

            var layoutDomain = await _layoutRepository.CreateAsync(layout);
            return LayoutMapper.GetLayoutDto(layoutDomain);
        }

        public async Task DeleteAsync(int layoutId)
        {
            ValidateIsIdMoreThanZero(layoutId);
            await ValidateIsItemExistanceAsync(layoutId);

            await _layoutRepository.DeleteAsync(layoutId);
        }

        public async Task<IQueryable<LayoutDto>> GetAllAsync()
        {
            var domainList = await _layoutRepository.GetAllAsync();

            var dtoList = domainList
                .Select(layout => LayoutMapper.GetLayoutDto(layout))
                .AsQueryable<LayoutDto>();

            return dtoList;
        }

        public async Task<LayoutDto> GetByIdAsync(int layoutId)
        {
            ValidateIsIdMoreThanZero(layoutId);
            await ValidateIsItemExistanceAsync(layoutId);

            var layoutDomain = await _layoutRepository.GetByIdAsync(layoutId);
            var layoutDto = LayoutMapper.GetLayoutDto(layoutDomain);

            return layoutDto;
        }

        public async Task<LayoutDto> UpdateAsync(LayoutDto item)
        {
            ValidateItemForNull(item);
            ValidateIsIdMoreThanZero(item.Id);
            ValidateModel(item);

            await ValidateIsItemExistanceAsync(item.Id);
            await ValidateIsVenueExistanceAsync(item.VenueId);
            await ValidateIsDescriptionUniqueAsync(item.Description, item.VenueId, item.Id);

            var layout = LayoutMapper.GetLayoutDomain(item);

            var layoutDomain = await _layoutRepository.UpdateAsync(layout);
            return LayoutMapper.GetLayoutDto(layoutDomain);
        }
    }
}