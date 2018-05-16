using System;
using System.Collections.Generic;
using System.Linq;

namespace SortExpenses.Extractions
{
    public static class Extract
    {
        private static List<DataExtractor<DateTime>> dataExtractors = new List<DataExtractor<DateTime>>()
        {            
            new ExtractDate_ddMMyyyyDashes(),
            new ExtractDate_ddMMyyyySlashes(),
            new ExtractDate_ddMMyySlashes(),
            new ExtractDate_ddMMyyDashes(),
            new ExtractDate_ddMMMyyyyDashes(), // met ce new en premier, et le bug disparait
        };

        public static IReadOnlyList<DateTime> AllDatesFrom(string content)
        {
            // BUG : les dates sont triées par l'ordre de déclaration des Extractor, et non par leur ordre d'apparition !
            return dataExtractors.SelectMany(extractor => extractor.Extract(content)).ToList();
        }
    }
}