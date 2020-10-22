using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Factories
{
    internal static class AreaFactory
    {
        public static AreaService MakeService()
        {
            var connectionString = DatabaseConnection.ConnectionString;
            var areaRepository = new AreaRepository(connectionString);
            var layoutRepository = new LayoutRepository(connectionString);
            var areaService = new AreaService(areaRepository, layoutRepository);

            return areaService;
        }
    }
}
