using System.Linq;
using SortExpenses.ExpensesReaders;
using SortExpenses.Extractions;
using SortExpenses.Folders;

namespace SortExpenses
{
    public class SortExpenses
    {
        private readonly ExpensesExtractor _extractor;

        public SortExpenses(IExpensesReader reader)
        {
            _extractor = new ExpensesExtractor(reader);
        }

        public ScannedFiles ByDate(IFolder folder)
        {
            var extractedFiles =  _extractor.GetFilesFrom(folder);

            return new ScannedFiles(
                extractedFiles
                    .Where(a => a.FoundDates.Count == 1)
                    .OrderBy(a => a.FoundDates.First())
                    .Select(a => a.FilePath).ToList(),
                
                extractedFiles
                    .Where(a=>!a.FoundDates.Any())
                    .Select(a=>a.FilePath).ToList(),

                extractedFiles
                    .Where(a=>a.FoundDates.Count > 1)
                    .Select(a=>a.FilePath)
                    .ToList());
        }
    }
}