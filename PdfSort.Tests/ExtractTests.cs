using System;
using System.Globalization;
using NFluent;
using NUnit.Framework;
using PdfSort.Extractions;

namespace PdfSort.Tests
{
    public class ExtractTests
    {
        [Test]
        public void TestExtractDate_ddMMMyyyyDashes()
        {
            var extract = new ExtractDate_ddMMMyyyyDashes();
            Check.That(extract.Extract("test 10-AVR-2017")).Not.IsEmpty();
        }

        [TestCase("10-APR-2018")]
        [TestCase("10-JAN-2018")]
        public void ParseDateWithTextMonth(string dateSource)
        {
            var culture = new CultureInfo("en");
            Console.WriteLine(culture.DateTimeFormat.AbbreviatedMonthNames);

            var date = DateTime.ParseExact(dateSource, "dd-MMM-yyyy", culture);
        }
    }

    public class Provider : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }
    }
}
