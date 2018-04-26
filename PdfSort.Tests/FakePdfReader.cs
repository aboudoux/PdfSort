using System.Collections.Generic;

namespace PdfSort.Tests
{
    public class FakePdfReader : IPdfReader
    {
        private readonly Dictionary<string, string> _files = new Dictionary<string, string>();

        public void AddFile(string fileName, string content) => _files.Add(fileName, content);

        public string Read(string pdfFilePath) => _files[pdfFilePath];
    }
}