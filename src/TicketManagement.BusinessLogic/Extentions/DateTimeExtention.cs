using System;

namespace Ticketmanagement.BusinessLogic.Extentions
{
    internal static class DateTimeExtention
    {
        /// <summary>
        /// Check if current datetime between lower limit DateTime and upper limit DateTime.
        /// </summary>
        /// <param name="current">Current DateTime.</param>
        /// <param name="lowerLimit">Lower limit of DateTime.</param>
        /// <param name="upperLimit">Upper limit of DateTime.</param>
        /// <returns>True if current DateTime is between.</returns>
        public static bool IsBetween(this DateTime current, DateTime lowerLimit, DateTime upperLimit)
        {
            return lowerLimit < current && current < upperLimit;
        }
    }
}
