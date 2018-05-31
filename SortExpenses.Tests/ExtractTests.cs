using System;
using System.Globalization;
using System.Linq;
using NFluent;
using NUnit.Framework;
using SortExpenses.Extractions;

namespace SortExpenses.Tests
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
        [TestCase("01-JAN-2018")]
        public void ParseDateWithTextMonth(string dateSource)
        {
            var culture = new CultureInfo("en");
            Console.WriteLine(culture.DateTimeFormat.AbbreviatedMonthNames);

            var date = DateTime.ParseExact(dateSource, "dd-MMM-yyyy", culture);
            Check.That(date).IsInstanceOf<DateTime>();
        }

        [TestCase("lundi 19 janvier 2018", "19/01/2018")]
        [TestCase("lundi 19 février 2018", "19/02/2018")]
        [TestCase("lundi 19 fevrier 2018", "19/02/2018")]
        [TestCase("lundi 19 FEVRIER 2018", "19/02/2018")]
        [TestCase("lundi 19 mars 2018", "19/03/2018")]
        [TestCase("lundi 19 avril 2018", "19/04/2018")]
        [TestCase("lundi 19 mai 2018", "19/05/2018")]
        [TestCase("lundi 19 juin 2018", "19/06/2018")]
        [TestCase("lundi 19 juillet 2018", "19/07/2018")]
        [TestCase("lundi 19 aout 2018", "19/08/2018")]
        [TestCase("lundi 19 AOUT 2018", "19/08/2018")]
        [TestCase("lundi 19 août 2018", "19/08/2018")]
        [TestCase("lundi 19 septembre 2018", "19/09/2018")]
        [TestCase("lundi 19 octobre 2018", "19/10/2018")]
        [TestCase("lundi 19 novembre 2018", "19/11/2018")]
        [TestCase("lundi 19 decembre 2018", "19/12/2018")]
        public void ParseFullDateWithTwoNumber(string dateSource, string expected)
        {            
            var extractor = new ExtractDateWithDayInTwoDigitsLongMonthNameAndYear();
            var parsedDate = extractor.Extract(dateSource).First().parsedData;
            Check.That(parsedDate).Equals(DateTime.Parse(expected));
        }

        [TestCase("lundi 9 janvier 2018", "9/01/2018")]
        [TestCase("lundi 9 février 2018", "9/02/2018")]
        [TestCase("lundi 9 fevrier 2018", "9/02/2018")]
        [TestCase("lundi 9 FEVRIER 2018", "9/02/2018")]
        [TestCase("lundi 9 mars 2018", "9/03/2018")]
        [TestCase("lundi 9 avril 2018", "9/04/2018")]
        [TestCase("lundi 9 mai 2018", "9/05/2018")]
        [TestCase("lundi 9 juin 2018", "9/06/2018")]
        [TestCase("lundi 9 juillet 2018", "9/07/2018")]
        [TestCase("lundi 9 aout 2018", "9/08/2018")]
        [TestCase("lundi 9 AOUT 2018", "9/08/2018")]
        [TestCase("lundi 9 août 2018", "9/08/2018")]
        [TestCase("lundi 9 septembre 2018", "9/09/2018")]
        [TestCase("lundi 9 octobre 2018", "9/10/2018")]
        [TestCase("lundi 9 novembre 2018", "9/11/2018")]
        [TestCase("lundi 9 decembre 2018", "9/12/2018")]
        public void ParseFullDateWithOneNumber(string dateSource, string expected)
        {
            var extractor = new ExtractDateWithDayInOneDigitLonMonthNameAndYear();
            var parsedDate = extractor.Extract(dateSource).First().parsedData;
            Check.That(parsedDate).Equals(DateTime.Parse(expected));
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
