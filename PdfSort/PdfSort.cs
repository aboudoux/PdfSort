using System.Linq;
using PdfSort.Extractions;

namespace PdfSort
{
    public class PdfSort
    {
        private PdfExtractor _extractor;

        public PdfSort(IPdfReader reader)
        {
            _extractor = new PdfExtractor(reader);
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