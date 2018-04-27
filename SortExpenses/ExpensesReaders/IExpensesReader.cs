namespace SortExpenses.ExpensesReaders
{
    public interface IExpensesReader
    {
        string Read(string pdfFilePath);
    }
}