using System.Collections.Generic;
using SortExpenses.ExpensesReaders;
using SortExpenses.Extractions;
using SortExpenses.Folders;
using SortExpenses.Sorting.Strategies;

namespace SortExpenses.Sorting
{
    public class SortExpenses
    {
        private readonly ExpensesExtractor _extractor;
        private IReadOnlyList<ExtractedFile> _extractedFiles;

        private SortExpenses(IExpensesReader reader)

        {
            _extractor = new ExpensesExtractor(reader);
        }

        public static SortExpenses WithReader(IExpensesReader reader) => new SortExpenses(reader);

        public SortExpenses ForFolder(IFolder folder)
        {
            _extractedFiles = _extractor.GetFilesFrom(folder);
            return this;
        }

        public ScannedFiles With<T>() where T : ISortingStrategy, new()
        {
            var strategy = new T();
            return new ScannedFiles(
                strategy.GetFilesSortedByDate(_extractedFiles),
                strategy.GetFilesWithoutDates(_extractedFiles),
                strategy.GetFilesWithMultipleDates(_extractedFiles));
        }
    }
}