using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfSort.Extractions
{
    public class PdfExtractor
    {
        private readonly IPdfReader _pdfReader;

        public PdfExtractor(IPdfReader pdfReader)
        {
            _pdfReader = pdfReader ?? throw new ArgumentNullException(nameof(pdfReader));
        }

        public IReadOnlyList<ExtractedFile> GetFilesFrom(IFolder folder)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));

            var pdfFiles = folder.GetPdfFiles();

            return pdfFiles
                .Select(file => new ExtractedFile(file, Dates(file)))
                .ToList();
        }

        private List<DateTime> Dates(string file)
        {
            var content = _pdfReader.Read(file);
            return Extract.AllDatesFrom(content).ToList();
        }
    }

    public class ExtractedFile
    {
        public ExtractedFile(string filePath, List<DateTime> foundDates)
        {
            FilePath = filePath;
            FoundDates = foundDates.Distinct(Elements.ByDate()).ToList();
        }

        public string FilePath { get; }
        public List<DateTime> FoundDates { get; }

        private class Elements : IEqualityComparer<DateTime>
        {
            public bool Equals(DateTime x, DateTime y) => x.Equals(y);

            public int GetHashCode(DateTime obj) => obj.GetHashCode();

            public static Elements ByDate() => new Elements();
        }
    }
}