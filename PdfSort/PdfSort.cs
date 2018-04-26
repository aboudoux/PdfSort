using System;
using System.Collections.Generic;
using System.Linq;
using PdfSort.Extractions;

namespace PdfSort
{
    public class PdfSort
    {
        private readonly IPdfReader _pdfReader;

        public PdfSort(IPdfReader pdfReader)
        {
            _pdfReader = pdfReader ?? throw new ArgumentNullException(nameof(pdfReader));
        }

        public IReadOnlyList<string> ByDates(IFolder folder)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));

            var pdfFiles = folder.GetPdfFiles();

            var parsedFiles = pdfFiles
                .SelectMany(Dates)
                .ToList();

            return
                parsedFiles
                    .OrderBy(a => a.Date)
                    .Select(a => a.FilePath)
                    .ToList(); 
        }

        private IEnumerable<ParsedFile> Dates(string file)
        {
            var content = _pdfReader.Read(file);
            return Extract.AllDatesFrom(content).Select(p => new ParsedFile(file, p));
        }

        private class ParsedFile
        {
            public ParsedFile(string filePath, DateTime date)
            {
                FilePath = filePath;
                Date = date;
            }

            public string FilePath { get; }
            public DateTime Date { get; }
        }
    }
}