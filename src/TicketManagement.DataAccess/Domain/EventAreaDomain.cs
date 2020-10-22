namespace TicketManagement.DataAccess.Models
{
    public class EventAreaDomain
    {
        /// <summary>
        /// Identifier of the event area.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of event witch has this event area.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Description of the event area.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Point of event area start location by X.
        /// </summary>
        public double CoordX { get; set; }

        /// <summary>
        /// Point of area start location by Y.
        /// </summary>
        public double CoordY { get; set; }

        /// <summary>
        /// Price for a ticket in this area.
        /// </summary>
        public decimal Price { get; set; }
    }
}
