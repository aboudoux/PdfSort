using System;
using FluentAssertions;
using NFluent;
using NUnit.Framework;

namespace PdfSort.Tests
{
    [TestFixture]
    public class PdfSortByDateShould
    {
        [Test]
        public void ReturnFilesSortedByDateAscendenting()
        {
            var reader = new FakePdfReader();
            reader.AddFile("test1.pdf", "01/07/2017");
            reader.AddFile("test2.pdf", "02/07/2016");

           var fakeFolder = new FakeFolder();
            fakeFolder.AddFile("test1.pdf");
            fakeFolder.AddFile("test2.pdf");

            var sort = new PdfSort(reader);
            var files = sort.ByDates(fakeFolder);

            files.Should().ContainInOrder("test2.pdf", "test1.pdf");
        }
    }
}
