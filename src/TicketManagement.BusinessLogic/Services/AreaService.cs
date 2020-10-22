using System.Linq;
using System.Threading.Tasks;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using Ticketmanagement.BusinessLogic.Services.Interfaces;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace Ticketmanagement.BusinessLogic.Services
{
    internal class AreaService : BaseValidationService<AreaDto>, IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ILayoutRepository _layoutRepository;

        public AreaService(IAreaRepository areaRepository, ILayoutRepository layoutRepository)
        {
            _areaRepository = areaRepository;
            _layoutRepository = layoutRepository;
        }

        private async Task ValidateIsLayoutIdExistanceAsync(int layoutId)
        {
            var allLayouts = await _layoutRepository.GetAllAsync();

            if (!allLayouts.Any(layout => layout.Id == layoutId))
            {
                throw new ItemExistenceException("layouts have not got the id");
            }
        }

        private async Task ValidateIsItemExistanceAsync(int id)
        {
            var allAreas = await _areaRepository.GetAllAsync();

            if (!allAreas.Any(area => area.Id == id))
            {
                throw new ItemExistenceException("areas have not got the id");
            }
        }

        private async Task ValidateIsDescriptionUniqueAsync(string description, int layoutId, int id = 0)
        {
            var areas = await _areaRepository.GetAllAsync();

            var isNotUniqueAreaForTheLayout = areas
                .Where(area => area.LayoutId == layoutId && area.Id != id)
                .Any(area => area.Description == description);

            if (isNotUniqueAreaForTheLayout)
            {
                throw new DescriptionUniqueException("areas description is not unique for a layout");
            }
        }

        public async Task<AreaDto> CreateAsync(AreaDto item)
        {
            ValidateItemForNull(item);
            ValidateModel(item);

            await ValidateIsLayoutIdExistanceAsync(item.LayoutId);
            await ValidateIsDescriptionUniqueAsync(item.Description, item.LayoutId);

            var area = AreaMapper.GetAreaDomain(item);

            var insertedArea = await _areaRepository.CreateAsync(area);
            return AreaMapper.GetAreaDto(insertedArea);
        }

        public async Task DeleteAsync(int areaId)
        {
            ValidateIsIdMoreThanZero(areaId);
            await ValidateIsItemExistanceAsync(areaId);

            await _areaRepository.DeleteAsync(areaId);
        }

        public async Task<IQueryable<AreaDto>> GetAllAsync()
        {
            var domainList = await _areaRepository.GetAllAsync();

            var dtoList = domainList
                .Select(area => AreaMapper.GetAreaDto(area))
                .AsQueryable<AreaDto>();

            return dtoList;
        }

        public async Task<AreaDto> GetByIdAsync(int areaId)
        {
            ValidateIsIdMoreThanZero(areaId);
            await ValidateIsItemExistanceAsync(areaId);

            var areaDomain = await _areaRepository.GetByIdAsync(areaId);
            var areaDto = AreaMapper.GetAreaDto(areaDomain);

            return areaDto;
        }

        public async Task<AreaDto> UpdateAsync(AreaDto item)
        {
            ValidateItemForNull(item);
            ValidateModel(item);
            ValidateIsIdMoreThanZero(item.Id);

            await ValidateIsDescriptionUniqueAsync(item.Description, item.LayoutId, item.Id);
            await ValidateIsItemExistanceAsync(item.Id);
            await ValidateIsLayoutIdExistanceAsync(item.LayoutId);

            var area = AreaMapper.GetAreaDomain(item);

            var updatedArea = await _areaRepository.UpdateAsync(area);
            return AreaMapper.GetAreaDto(updatedArea);
        }
    }
}