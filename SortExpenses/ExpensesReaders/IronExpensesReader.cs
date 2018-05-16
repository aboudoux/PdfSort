using IronOcr;

namespace SortExpenses.ExpensesReaders
{
    public class IronExpensesReader : IExpensesReader
    {
        private readonly AutoOcr _ocrEngine = new AutoOcr();

        public string Read(string pdfFilePath)
        {
            var result = _ocrEngine.ReadPdf(pdfFilePath);
            return result.Text;
        }
    }
}