using System.Collections.Generic;
using PdfSort.Extractions;

namespace PdfSort.Tests.Tools
{
    public class TestPdfSort
    {
        private readonly FakePdfReader _pdfReader = new FakePdfReader();
        private readonly FakeFolder _folder = new FakeFolder();

        private TestPdfSort()
        {
        }

        public static TestPdfSort Create() => new TestPdfSort();

        public TestPdfSort WithFile(string fileName, string content)
        {
            _pdfReader.AddFile(fileName, content);
            _folder.AddFile(fileName);
            return this;
        }

        public PdfSortResult ExecuteSortByDates() => new PdfSort(_pdfReader).ByDate(_folder);
    }
}