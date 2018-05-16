using System.Collections.Generic;
using System.Linq;
using SortExpenses.Extractions;

namespace SortExpenses.Sorting.Strategies
{
    public class SortByDate : ISortingStrategy
    {
        public virtual IReadOnlyList<string> GetFilesSortedByDate(IReadOnlyList<ExtractedFile> extractedFiles)
        {
            return extractedFiles
                .Where(a => a.FoundDates.Count == 1)
                .OrderBy(a => a.FoundDates.First())
                .Select(a => a.FilePath).ToList();
        }

        public IReadOnlyList<string> GetFilesWithMultipleDates(IReadOnlyList<ExtractedFile> extractedFiles)
        {
            return extractedFiles
                .Where(a => a.FoundDates.Count > 1)
                .Select(a => a.FilePath)
                .ToList();
        }

        public IReadOnlyList<string> GetFilesWithoutDates(IReadOnlyList<ExtractedFile> extractedFiles)
        {
            return extractedFiles
                .Where(a => !a.FoundDates.Any())
                .Select(a => a.FilePath).ToList();
        }
    }
}