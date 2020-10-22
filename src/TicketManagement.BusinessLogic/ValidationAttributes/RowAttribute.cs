using System.ComponentModel.DataAnnotations;

namespace Ticketmanagement.BusinessLogic.ValidationAttributes
{
    public class RowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is int && (int)value >= Constants.BottomLimitOfRow;
        }
    }
}
