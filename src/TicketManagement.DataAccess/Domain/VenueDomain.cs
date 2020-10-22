namespace TicketManagement.DataAccess.Models
{
    public class VenueDomain
    {
        /// <summary>
        /// Identifier of the venue.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Description of the venue.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Addres of the venue.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Phone for connection with the venue.
        /// </summary>
        public string Phone { get; set; }
    }
}
