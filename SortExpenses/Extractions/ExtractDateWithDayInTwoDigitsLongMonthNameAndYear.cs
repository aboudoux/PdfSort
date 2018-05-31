using System;
using System.Collections.Generic;
using System.Linq;

namespace SortExpenses.Extractions
{
    public class ExtractDateWithDayInTwoDigitsLongMonthNameAndYear : DataExtractor<DateTime>
    {
        private readonly Dictionary<string, string> _longMonthMapping = new Dictionary<string, string>()
        {
            {"janvier", "january"},
            {"fevrier", "february"},
            {"février", "february"},
            {"mars", "march"},
            {"avril", "april"},
            {"mai", "may"},
            {"juin", "june"},
            {"juillet", "july"},
            {"aout", "august"},
            {"août", "august"},
            {"septembre", "september"},
            {"octobre", "october"},
            {"novembre", "november"},
            {"decembre", "december"},
        };

        protected override string RegexPattern => @"\d{1,2} \w* 20\d{2}";
        protected override DateTime Parse(string data) => Normalize(data.ToLower()).ToDate(DateExtentions.ddMMMMyyyyWhitespace);

        protected string Normalize(string format) => _longMonthMapping.Aggregate(format, (current, pair) => current.Replace(pair.Key, pair.Value));
    }
}