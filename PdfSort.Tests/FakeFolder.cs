using System.Collections.Generic;
using System.Linq;

namespace PdfSort.Tests
{
    public class FakeFolder : IFolder
    {
        private readonly HashSet<string> _files = new HashSet<string>();

        public void AddFile(string fileName) => _files.Add(fileName);

        public string[] GetPdfFiles() => _files.ToArray();
    }
}