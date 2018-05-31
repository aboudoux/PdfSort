using System;
using System.Collections.Generic;
using System.Linq;

namespace SortExpenses.Extractions
{
    public static class Extract
    {
        private static readonly List<DataExtractor<DateTime>> DataExtractors = new List<DataExtractor<DateTime>>()
        {            
            new ExtractDate_ddMMyyyyDashes(),
            new ExtractDate_ddMMyyyySlashes(),
            new ExtractDate_ddMMyySlashes(),
            new ExtractDate_ddMMyyDashes(),
            new ExtractDate_ddMMMyyyyDashes(),
            new ExtractDateWithDayInOneDigitLonMonthNameAndYear(),
            new ExtractDateWithDayInTwoDigitsLongMonthNameAndYear()
        };

        public static IReadOnlyList<DateTime> AllDatesFrom(string content)
        {
            return DataExtractors.SelectMany(extractor => extractor.Extract(content)).WithSameOrderOf(content);
        }

        public static IReadOnlyList<DateTime> WithSameOrderOf(this IEnumerable<(string source, DateTime parsedData)> tuple, string content)
        {
            return tuple.Select(
                a => (content.IndexOf(a.source, StringComparison.Ordinal),a.parsedData))
                .OrderBy(b=>b.Item1)
                .Select(a=>a.Item2)
                .ToList();
        }
    }
}