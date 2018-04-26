using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfSort.Extractions
{
    public static class Extract
    {
        private static List<DataExtractor<DateTime>> dataExtractors = new List<DataExtractor<DateTime>>()
        {
            new ExtractDate_ddMMyyyySlashes(),
            new ExtractDate_ddMMyyyyDashes(),
            new ExtractDate_ddMMyySlashes(),
            new ExtractDate_ddMMyyDashes(),
            new ExtractDate_ddMMMyyyyDashes(),
        };

        public static IReadOnlyList<DateTime> AllDatesFrom(string content)
        {
            return dataExtractors.SelectMany(a => a.Extract(content)).ToList();
        }
    }
}