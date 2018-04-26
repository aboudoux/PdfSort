using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using PdfSort.Extractions;

namespace PdfSort.Tests
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
        };

        [Test, TestCaseSource(nameof(_datesCases))]
        public void GetAllDatesFromContent(string content, List<DateTime> expectedDates)
        {
            var dates = Extract.AllDatesFrom(content);
            Check.That(dates).Contains(expectedDates);
        }
    }
}