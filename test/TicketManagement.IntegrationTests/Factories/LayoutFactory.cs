using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Factories
{
    internal static class LayoutFactory
    {
        public static LayoutService MakeService()
        {
            var connectionString = DatabaseConnection.ConnectionString;
            var layoutRepository = new LayoutRepository(connectionString);
            var venueRepository = new VenueRepository(connectionString);
            var layoutService = new LayoutService(layoutRepository, venueRepository);

            return layoutService;
        }
    }
}
