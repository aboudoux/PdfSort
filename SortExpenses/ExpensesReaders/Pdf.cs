using System;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace SortExpenses.ExpensesReaders
{
    public class Pdf : IDisposable
    {
        private readonly PdfReader _reader;
        private readonly ITextExtractionStrategy _strategy = new SimpleTextExtractionStrategy();
        private readonly Encoding _iso88591 = Encoding.GetEncoding("ISO-8859-1");

        private Pdf(string fileName)
        {
            _reader = new PdfReader(fileName);
        }

        public static Pdf Open(string fileName)
        {
            return new Pdf(fileName);
        }

        public string ReadAllPages()
        {
            var sb = new StringBuilder();

            for (var page = 0; page < _reader.NumberOfPages; page++)
            {
                var text = PdfTextExtractor.GetTextFromPage(_reader, page + 1, _strategy);
                if (!string.IsNullOrWhiteSpace(text))
                    sb.Append(_iso88591.GetString(bytes: Encoding.Convert(_iso88591, _iso88591, _iso88591.GetBytes(text))));
            }

            return sb.ToString();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}