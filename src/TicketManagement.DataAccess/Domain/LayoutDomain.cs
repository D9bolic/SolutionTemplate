namespace TicketManagement.DataAccess.Models
{
    public class LayoutDomain
    {
        /// <summary>
        /// Identifier of the layout.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of venue witch has this layout.
        /// </summary>
        public int VenueId { get; set; }

        /// <summary>
        /// Description of this layout.
        /// </summary>
        public string Description { get; set; }
    }
}
