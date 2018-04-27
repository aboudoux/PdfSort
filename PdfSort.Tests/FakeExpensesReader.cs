using System.Collections.Generic;
using SortExpenses.ExpensesReaders;

namespace SortExpenses.Tests
{
    public class FakeExpensesReader : IExpensesReader
    {
        private readonly Dictionary<string, string> _files = new Dictionary<string, string>();

        public void AddFile(string fileName, string content) => _files.Add(fileName, content);

        public string Read(string pdfFilePath) => _files[pdfFilePath];
    }
}