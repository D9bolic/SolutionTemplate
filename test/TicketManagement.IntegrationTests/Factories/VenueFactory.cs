using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Factories
{
    internal static class VenueFactory
    {
        public static VenueService MakeService()
        {
            var connectionString = DatabaseConnection.ConnectionString;
            var venueRepository = new VenueRepository(connectionString);
            var venueService = new VenueService(venueRepository);

            return venueService;
        }
    }
}
