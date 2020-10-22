using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Models;

namespace Ticketmanagement.BusinessLogic.Mappers
{
    internal static class AreaMapper
    {
        /// <summary>
        /// Get area domain model with the same fields.
        /// </summary>
        public static AreaDomain GetAreaDomain(AreaDto areaDto)
        {
            var areaDomain = new AreaDomain
            {
                Id = areaDto.Id,
                LayoutId = areaDto.LayoutId,
                Description = areaDto.Description,
                CoordX = areaDto.CoordX,
                CoordY = areaDto.CoordY,
            };

            return areaDomain;
        }

        /// <summary>
        /// Get area dto model with the same fields.
        /// </summary>
        public static AreaDto GetAreaDto(AreaDomain areaDomain)
        {
            var areaDto = new AreaDto
            {
                Id = areaDomain.Id,
                LayoutId = areaDomain.LayoutId,
                Description = areaDomain.Description,
                CoordX = areaDomain.CoordX,
                CoordY = areaDomain.CoordY,
            };

            return areaDto;
        }
    }
}
