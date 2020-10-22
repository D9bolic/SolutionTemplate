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
    internal class SeatService : BaseValidationService<SeatDto>, ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IAreaRepository _areaRepository;

        public SeatService(ISeatRepository seatRepository, IAreaRepository areaRepository)
        {
            _seatRepository = seatRepository;
            _areaRepository = areaRepository;
        }

        private async Task ValidateIsAreaExistanceAsync(int areaId)
        {
            var allAreas = await _areaRepository.GetAllAsync();

            if (!allAreas.Any(area => area.Id == areaId))
            {
                throw new ItemExistenceException("areas have not got the id");
            }
        }

        private async Task ValidateIsItemExistanceAsync(int id)
        {
            var allVenues = await _seatRepository.GetAllAsync();

            if (!allVenues.Any(seat => seat.Id == id))
            {
                throw new ItemExistenceException("seats have not got the id");
            }
        }

        private async Task ValidateIsSeatPositionUniqueAsync(int areaId, int row, int number, int id = 0)
        {
            var seats = await _seatRepository.GetAllAsync();

            // does position of seat(row and number) is unique in the same area
            var isSeatPositionNotUnique = seats
                .Where(seat => seat.AreaId == areaId && seat.Id != id)
                .Any(seat => (seat.Row == row) && (seat.Number == number));

            if (isSeatPositionNotUnique)
            {
                throw new UniquePositionException();
            }
        }

        public async Task<SeatDto> CreateAsync(SeatDto item)
        {
            ValidateItemForNull(item);
            ValidateModel(item);

            await ValidateIsSeatPositionUniqueAsync(item.AreaId, item.Row, item.Number);
            await ValidateIsAreaExistanceAsync(item.AreaId);

            var seat = SeatMapper.GetSeatDomain(item);

            var insertedSeat = await _seatRepository.CreateAsync(seat);
            return SeatMapper.GetSeatDto(insertedSeat);
        }

        public async Task DeleteAsync(int seatId)
        {
            ValidateIsIdMoreThanZero(seatId);
            await ValidateIsItemExistanceAsync(seatId);

            await _seatRepository.DeleteAsync(seatId);
        }

        public async Task<IQueryable<SeatDto>> GetAllAsync()
        {
            var domainList = await _seatRepository.GetAllAsync();

            var dtoList = domainList
                .Select(seat => SeatMapper.GetSeatDto(seat))
                .AsQueryable<SeatDto>();

            return dtoList;
        }

        public async Task<SeatDto> GetByIdAsync(int seatId)
        {
            ValidateIsIdMoreThanZero(seatId);
            await ValidateIsItemExistanceAsync(seatId);

            var seatDomain = await _seatRepository.GetByIdAsync(seatId);
            var seatDto = SeatMapper.GetSeatDto(seatDomain);

            return seatDto;
        }

        public async Task<SeatDto> UpdateAsync(SeatDto item)
        {
            ValidateItemForNull(item);
            ValidateIsIdMoreThanZero(item.Id);
            ValidateModel(item);

            await ValidateIsItemExistanceAsync(item.Id);
            await ValidateIsSeatPositionUniqueAsync(item.AreaId, item.Row, item.Number, item.Id);
            await ValidateIsAreaExistanceAsync(item.AreaId);

            var seat = SeatMapper.GetSeatDomain(item);

            var updatedSeat = await _seatRepository.UpdateAsync(seat);
            return SeatMapper.GetSeatDto(updatedSeat);
        }
    }
}