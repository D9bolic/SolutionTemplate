using System.ComponentModel.DataAnnotations;

namespace Ticketmanagement.BusinessLogic.DTO
{
    internal class LayoutDto
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
        [Required(ErrorMessage = ValidationErrorMessage.DescriptionNullError)]
        public string Description { get; set; }
    }
}
