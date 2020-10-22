namespace TicketManagement.DataAccess.Models
{
    public class SeatDomain
    {
        /// <summary>
        /// Identifier of the seat.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of area witch has this seat.
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// Row of the seat at the area.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Number of the seat at the area.
        /// </summary>
        public int Number { get; set; }
    }
}
