using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Factories
{
    internal static class EventAreaFactory
    {
        public static EventAreaService MakeService()
        {
            var connectionString = DatabaseConnection.ConnectionString;
            var eventAreaRepository = new EventAreaRepository(connectionString);
            var eventAreaService = new EventAreaService(eventAreaRepository);

            return eventAreaService;
        }
    }
}
