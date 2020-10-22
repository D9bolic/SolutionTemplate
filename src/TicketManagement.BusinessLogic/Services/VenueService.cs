using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using Ticketmanagement.BusinessLogic.Services.Interfaces;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace Ticketmanagement.BusinessLogic.Services
{
    internal class VenueService : BaseValidationService<VenueDto>, IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        private async Task ValidateIsItemExistanceAsync(int id)
        {
            var allVenues = await _venueRepository.GetAllAsync();

            if (!allVenues.Any(venue => venue.Id == id))
            {
                throw new ItemExistenceException("venues have not got the id");
            }
        }

        private async Task ValidateIsDescriptionUniqueAsync(string description, int id = 0)
        {
            var venues = await _venueRepository.GetAllAsync();

            var isDescriptionNotUnique = venues
                .Where(venue => venue.Id != id)
                .Any(venue => venue.Description == description);

            if (isDescriptionNotUnique)
            {
                throw new DescriptionUniqueException("venues description is not unique");
            }
        }

        public async Task<VenueDto> CreateAsync(VenueDto item)
        {
            ValidateItemForNull(item);
            ValidateModel(item);

            await ValidateIsDescriptionUniqueAsync(item.Description);

            var venue = VenueMapper.GetVenueDomain(item);
            var venueDomain = await _venueRepository.CreateAsync(venue);

            return VenueMapper.GetVenueDto(venueDomain);
        }

        public async Task DeleteAsync(int venueId)
        {
            ValidateIsIdMoreThanZero(venueId);
            await ValidateIsItemExistanceAsync(venueId);

            await _venueRepository.DeleteAsync(venueId);
        }

        public async Task<IQueryable<VenueDto>> GetAllAsync()
        {
            var domainList = await _venueRepository.GetAllAsync();

            var dtoList = domainList
                .Select(venue => VenueMapper.GetVenueDto(venue))
                .AsQueryable<VenueDto>();

            return dtoList;
        }

        public async Task<VenueDto> GetByIdAsync(int venueId)
        {
            ValidateIsIdMoreThanZero(venueId);
            await ValidateIsItemExistanceAsync(venueId);

            var venueDomain = await _venueRepository.GetByIdAsync(venueId);
            var venueDto = VenueMapper.GetVenueDto(venueDomain);

            return venueDto;
        }

        public async Task<VenueDto> UpdateAsync(VenueDto item)
        {
            ValidateItemForNull(item);
            ValidateModel(item);
            ValidateIsIdMoreThanZero(item.Id);

            await ValidateIsItemExistanceAsync(item.Id);
            await ValidateIsDescriptionUniqueAsync(item.Description, item.Id);

            var oldVenue = VenueMapper.GetVenueDomain(item);

            var venueDomain = await _venueRepository.UpdateAsync(oldVenue);
            return VenueMapper.GetVenueDto(venueDomain);
        }
    }
}