using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Factories
{
    internal static class EventSeatFactory
    {
        public static EventSeatService MakeService()
        {
            var connectionString = DatabaseConnection.ConnectionString;
            var eventSeatRepository = new EventSeatRepository(connectionString);
            var eventSeatService = new EventSeatService(eventSeatRepository);

            return eventSeatService;
        }
    }
}
