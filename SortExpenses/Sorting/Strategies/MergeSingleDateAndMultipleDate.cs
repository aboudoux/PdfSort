using System.Collections.Generic;
using System.Linq;
using SortExpenses.Extractions;

namespace SortExpenses.Sorting.Strategies
{
    public class MergeSingleDateAndMultipleDate : SortByDate
    {
        public override IReadOnlyList<string> GetFilesSortedByDate(IReadOnlyList<ExtractedFile> extractedFiles)
        {
            return extractedFiles
                .Where(a => a.FoundDates.Any())
                .OrderBy(a => a.FoundDates.First())
                .Select(a => a.FilePath).ToList();
        }
    }
}