using System.ComponentModel.DataAnnotations;

namespace Ticketmanagement.BusinessLogic.DTO
{
    internal class VenueDto
    {
        /// <summary>
        /// Identifier of the venue.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Description of the venue.
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.DescriptionNullError)]
        public string Description { get; set; }

        /// <summary>
        /// Addres of the venue.
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.AddressNullError)]
        public string Address { get; set; }

        /// <summary>
        /// Phone for connection with the venue.
        /// </summary>
        [RegularExpression(Constants.PhonePattern, ErrorMessage = ValidationErrorMessage.IncorrectPhoneError)]
        public string Phone { get; set; }
    }
}
