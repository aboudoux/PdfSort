using System;
using System.Collections.Generic;
using FluentAssertions;
using NFluent;
using NUnit.Framework;
using SortExpenses.Extractions;

namespace SortExpenses.Tests
{
    [TestFixture]
    public class ExtractionShould
    {
        private static object[] _datesCases =
        {
            new object[] {"test 01/07/2006", new List<DateTime>() {new DateTime(2006, 7, 1)}},
            new object[] {"test 02/07/2007 coucou 01/01/2017", new List<DateTime>() {new DateTime(2007, 7, 2), new DateTime(2017,1,1)}},
            new object[] {"test 01-10-2016", new List<DateTime>() {new DateTime(2016, 10, 1)}},
            new object[] {"test 14-04-18", new List<DateTime>() {new DateTime(2018,4, 14)}},
            new object[] {"test 16-AVR-2018", new List<DateTime>() {new DateTime(2018,4, 16)}},
            new object[] {"test 10-mai-2018", new List<DateTime>() {new DateTime(2018,5, 10)}},
            new object[] {"test du lundi 8 juillet 2018", new List<DateTime>(){ new DateTime(2018,07,08)}, } ,
            new object[] {"test du mardi 12 mars 2018", new List<DateTime>(){ new DateTime(2018,03,12)}, } 
        };

        [Test, TestCaseSource(nameof(_datesCases))]
        public void GetAllDatesFromContent(string content, List<DateTime> expectedDates)
        {
            var dates = Extract.AllDatesFrom(content);
            Check.That(dates).Contains(expectedDates);
        }

        [Test]
        [TestCase("ceci est un test 01/07/2017 -- coucou")]
        public void GetOneDateFromContent(string content)
        {
            var dates = Extract.AllDatesFrom(content);
            dates.Should().HaveCount(1);
        }

        [Test]
        public void ExtractDatesInSameOrder()
        {
            var content = "ici le 10-mai-2017 pour le 15/01/2016 au 20-01-16 se déroulera le 12/02/18";
            var dates = Extract.AllDatesFrom(content);

            dates.Should().HaveCount(4);
            dates.Should().ContainInOrder(
                new DateTime(2017, 05, 10), 
                new DateTime(2016, 01, 15),
                new DateTime(2016, 01, 20), 
                new DateTime(2018, 02, 12));
        }
    }
}