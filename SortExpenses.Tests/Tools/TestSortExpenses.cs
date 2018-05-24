using SortExpenses.Sorting.Strategies;

namespace SortExpenses.Tests.Tools
{
    public class TestSortExpenses
    {
        private readonly FakeExpensesReader _expensesReader = new FakeExpensesReader();
        private readonly FakeFolder _folder = new FakeFolder();

        private TestSortExpenses()
        {
        }

        public static TestSortExpenses Create() => new TestSortExpenses();

        public TestSortExpenses WithFile(string fileName, string content)
        {
            _expensesReader.AddFile(fileName, content);
            _folder.AddFile(fileName);
            return this;
        }

        public ScannedFiles ExecuteSortByDates() =>
            Sorting.SortExpenses
                .WithReader(_expensesReader)
                .ForFolder(_folder)
                .With<SortByDate>();

        public ScannedFiles ExecuteMergedSortByDates() =>
            Sorting.SortExpenses
                .WithReader(_expensesReader)
                .ForFolder(_folder)
                .With<MergeSingleDateAndMultipleDate>();
    }
}