namespace SortExpenses.ExpensesReaders
{
    public class SimpleExpensesReader : IExpensesReader
    {
        public string Read(string pdfFilePath)
        {
            using (var pdfFile = Pdf.Open(pdfFilePath))
                return pdfFile.ReadAllPages();
        }
    }
}