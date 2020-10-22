using Ticketmanagement.BusinessLogic.ValidationAttributes;

namespace Ticketmanagement.BusinessLogic.DTO
{
    internal class SeatDto
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
        [Number(ErrorMessage = ValidationErrorMessage.NotPossitiveRowError)]
        public int Row { get; set; }

        /// <summary>
        /// Number of the seat at the area.
        /// </summary>
        [Number(ErrorMessage = ValidationErrorMessage.NotPossitiveNumberError)]
        public int Number { get; set; }
    }
}
