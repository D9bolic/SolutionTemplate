using System.ComponentModel.DataAnnotations;

namespace Ticketmanagement.BusinessLogic.ValidationAttributes
{
    public class NumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is int && (int)value >= Constants.BottomLimitOfNumber;
        }
    }
}
