using System;
using FluentAssertions;
using NUnit.Framework;
using SortExpenses.ExpensesReaders;

namespace SortExpenses.Tests
{
    [TestFixture]
    public class SimpleExpenseReaderShould
    {
        [Test]
        public void ReadSimplePdfFile()
        {
            var reader = new SimpleExpensesReader();
            var data = reader.Read("..\\..\\NDF\\1.pdf");

            data.Should().NotBeEmpty();
            data.Should().StartWith("Crédit");
        }
    }
}