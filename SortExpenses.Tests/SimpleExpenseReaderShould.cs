using System;
using FluentAssertions;
using NUnit.Framework;
using SortExpenses.ExpensesReaders;
using SortExpenses.Tests.Tools;

namespace SortExpenses.Tests
{
    [TestFixture]
    public class SimpleExpenseReaderShould
    {
        [Test]
        [TestCase(1, "Crédit")]
        public void ReadSomePdf(int pdfNumber, string expectedStart)
        {
            var reader = new SimpleExpensesReader();
            var data = reader.Read(Expenses.Pdf(pdfNumber));

            data.Should().NotBeEmpty();
            data.Should().StartWith(expectedStart);
        }
    }
}