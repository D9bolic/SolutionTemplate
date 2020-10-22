using System.Configuration;

namespace TicketManagement.IntegrationTests
{
    internal static class DatabaseConnection
    {
        /// <summary>
        /// Return connection string from configuration file for TestDatabase.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString;
            }
        }
    }
}
