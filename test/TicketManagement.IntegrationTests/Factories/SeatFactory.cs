using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Factories
{
    internal static class SeatFactory
    {
        public static SeatService MakeService()
        {
            var connectionString = DatabaseConnection.ConnectionString;
            var seatRepository = new SeatRepository(connectionString);
            var areaRepository = new AreaRepository(connectionString);
            var seatService = new SeatService(seatRepository, areaRepository);

            return seatService;
        }
    }
}
