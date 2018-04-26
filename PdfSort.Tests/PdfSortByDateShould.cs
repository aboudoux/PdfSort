using System;
using System.Collections.ObjectModel;
using FluentAssertions;
using NFluent;
using NUnit.Framework;
using PdfSort.Tests.Tools;

namespace PdfSort.Tests
{
    [TestFixture]
    public class PdfSortByDateShould
    {
        [Test]
        public void Return_Files_Sorted_By_Date_Ascendenting_When_Contains_One_Date()
        {
            var files = TestPdfSort
                .Create()
                .WithFile("test1.pdf", "ceci est un test 01/07/2017 -- coucou")
                .WithFile("test2.pdf", "NDF du 02-07-2016!")
                .WithFile("test3.pdf", "blabla ==05-07-16")
                .WithFile("test4.pdf", "--05-AVR-2012--")
                .WithFile("test6.pdf", "[05/07/12] well")
                .ExecuteSortByDates();

            files.FilesSortedByDates.Should().ContainInOrder(
                "test4.pdf",
                "test6.pdf",
                "test2.pdf", 
                "test3.pdf", 
                "test1.pdf");
        }

        [Test]
        public void Return_files_where_dates_are_not_found()
        {
            var files = TestPdfSort
                .Create()
                .WithFile("un1.pdf", "pas de date ici")
                .WithFile("un2.pdf", "ni la")
                .ExecuteSortByDates();

            throw new Exception();
        }
    }
}
