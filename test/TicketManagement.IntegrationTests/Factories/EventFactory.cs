using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Factories
{
    internal static class EventFactory
    {
        public static EventService MakeService()
        {
            var connectionString = DatabaseConnection.ConnectionString;

            var eventRepository = new EventRepository(connectionString);
            var areaRepository = new AreaRepository(connectionString);
            var seatRepository = new SeatRepository(connectionString);
            var layoutRepository = new LayoutRepository(connectionString);

            var eventServis = new EventService(eventRepository, areaRepository, seatRepository, layoutRepository);

            return eventServis;
        }
    }
}
