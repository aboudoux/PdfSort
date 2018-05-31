using System;

namespace SortExpenses.Extractions
{
    public class ExtractDateWithDayInOneDigitLonMonthNameAndYear : ExtractDateWithDayInTwoDigitsLongMonthNameAndYear
    {
        protected override DateTime Parse(string data) => Normalize(data.ToLower()).ToDate(DateExtentions.dMMMMyyyyWhitespace);
    }
}