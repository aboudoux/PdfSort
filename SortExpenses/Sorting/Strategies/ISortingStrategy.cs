using System.Collections.Generic;
using SortExpenses.Extractions;

namespace SortExpenses.Sorting.Strategies
{
    public interface ISortingStrategy
    {
        IReadOnlyList<string> GetFilesSortedByDate(IReadOnlyList<ExtractedFile> extractedFiles);
        IReadOnlyList<string> GetFilesWithMultipleDates(IReadOnlyList<ExtractedFile> extractedFiles);
        IReadOnlyList<string> GetFilesWithoutDates(IReadOnlyList<ExtractedFile> extractedFiles);
    }
}