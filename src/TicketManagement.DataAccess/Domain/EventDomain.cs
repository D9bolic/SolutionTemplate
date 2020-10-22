using System;

namespace TicketManagement.DataAccess.Models
{
    public class EventDomain
    {
        /// <summary>
        /// Identifier of the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of layout witch has this event.
        /// </summary>
        public int LayoutId { get; set; }

        /// <summary>
        /// Name of this event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of this event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Start date and time for the event.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Finish date and time for the event.
        /// </summary>
        public DateTime FinishDateTime { get; set; }
    }
}
