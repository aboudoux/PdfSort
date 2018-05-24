using FluentAssertions;
using NUnit.Framework;
using SortExpenses.ExpensesReaders;
using SortExpenses.Folders;
using SortExpenses.Sorting.Strategies;
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
                .WithFile("un2.pdf", "essai 12-AVR-2018 + du 15/02/17")
                .WithFile("un3.pdf", "essai 17/01/2017")
                .ExecuteMergedSortByDates();


            files.WithMultipleDate.Should().HaveCount(2);
            files.SortedByDates.Should().HaveCount(3).And.ContainInOrder("un3.pdf", "un1.pdf", "un2.pdf");
            files.WithoutDate.Should().BeEmpty();
        }
       
        [Test]
        public void sort_real_pdf_files_with_simple_read()
        {
            var result = Sorting.SortExpenses
                .WithReader(new SimpleExpensesReader())
                .ForFolder(new Folder(Expenses.Folder))
                .With<SortByDate>();

            result.SortedByDates.Should().NotBeEmpty();
            result.WithMultipleDate.Should().NotBeEmpty();
            result.WithoutDate.Should().NotBeEmpty();
        }
    }
}
