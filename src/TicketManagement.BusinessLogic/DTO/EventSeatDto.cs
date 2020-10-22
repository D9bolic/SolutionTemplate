using TicketManagement.DataAccess.Enums;

namespace Ticketmanagement.BusinessLogic.DTO
{
    internal class EventSeatDto
    {
        /// <summary>
        /// Identifier of the event seat.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of event area witch has this event seat.
        /// </summary>
        public int EventAreaId { get; set; }

        /// <summary>
        /// Row of the event seat at the event area.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Number of the event seat at the event area.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Bying state of this event seat.
        /// </summary>
        public EventSeatState State { get; set; }
    }
}
