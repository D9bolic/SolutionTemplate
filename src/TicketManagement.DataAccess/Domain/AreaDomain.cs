namespace TicketManagement.DataAccess.Models
{
    public class AreaDomain
    {
        /// <summary>
        /// Identifier of the area.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of layout witch has this area.
        /// </summary>
        public int LayoutId { get; set; }

        /// <summary>
        /// Description of the area.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Point of area start location by X.
        /// </summary>
        public int CoordX { get; set; }

        /// <summary>
        /// Point of area start location by Y.
        /// </summary>
        public int CoordY { get; set; }
    }
}
