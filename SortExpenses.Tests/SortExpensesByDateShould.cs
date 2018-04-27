using FluentAssertions;
using NUnit.Framework;
using SortExpenses.Tests.Tools;

namespace SortExpenses.Tests
{
    [TestFixture]
    public class SortExpensesByDateShould
    {
        [Test]
        public void Return_Files_Sorted_By_Date_Ascendenting_When_Contains_One_Date()
        {
            var files = TestSortExpenses
                .Create()
                .WithFile("test1.pdf", "ceci est un test 01/07/2017 -- coucou")
                .WithFile("test2.pdf", "NDF du 02-07-2016!")
                .WithFile("test3.pdf", "blabla ==05-07-16")
                .WithFile("test4.pdf", "--05-AVR-2012--")
                .WithFile("test6.pdf", "[05/07/12] well")
                .ExecuteSortByDates();

            files.SortedByDates.Should().ContainInOrder(
                "test4.pdf",
                "test6.pdf",
                "test2.pdf", 
                "test3.pdf", 
                "test1.pdf");
        }

        [Test]

        public void Return_Files_Sorted_By_Date_Ascendenting_When_Contains_thw_same_date()
        {
            var files = TestSortExpenses
                .Create()
                .WithFile("test1.pdf", "ceci est un test 01/07/2017 -- coucou 01/07/2017")
                .WithFile("test2.pdf", "NDF du 02-07-2016! and 02/07/2016")
                .WithFile("test3.pdf", "blabla ==05-07-16 ok 05-07-2016")
                .WithFile("test4.pdf", "--05-AVR-2012-- essai 05-04-12")
                .WithFile("test6.pdf", "[05/07/12] well")
                .ExecuteSortByDates();

            files.SortedByDates.Should().ContainInOrder(
                "test4.pdf",
                "test6.pdf",
                "test2.pdf",
                "test3.pdf",
                "test1.pdf");
        }


        [Test]
        public void Return_files_where_dates_are_not_found()
        {
            var files = TestSortExpenses
                .Create()
                .WithFile("un1.pdf", "pas de date ici")
                .WithFile("un2.pdf", "ni la")
                .ExecuteSortByDates();

            files.WithoutDate.Should().HaveCount(2);
            files.SortedByDates.Should().BeEmpty();
            files.WithMultipleDate.Should().BeEmpty();
        }

        [Test]
        public void Return_files_with_multiple_dates_found()
        {
            var files = TestSortExpenses
                .Create()
                .WithFile("un1.pdf", "date du 19/02/2018 et 10-10-18")
                .WithFile("un2.pdf", "essai 12-AVR-2018 + du 23/02/17")
                .ExecuteSortByDates();


            files.WithMultipleDate.Should().HaveCount(2);
            files.SortedByDates.Should().BeEmpty();
            files.WithoutDate.Should().BeEmpty();
        }
    }
}
