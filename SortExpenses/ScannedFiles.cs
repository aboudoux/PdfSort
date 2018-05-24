using System;
using System.Collections.Generic;

namespace SortExpenses
{
    public class ScannedFiles
    {
        public ScannedFiles(IReadOnlyList<(string fileName, DateTime retainedDate)> sortedByDates, IReadOnlyList<string> withoutDate, IReadOnlyList<string> withMultipleDate)
        {
            SortedByDates = sortedByDates;
            WithoutDate = withoutDate;
            WithMultipleDate = withMultipleDate;
        }

        public IReadOnlyList<(string fileName, DateTime retainedDate)> SortedByDates { get; }
        public IReadOnlyList<string> WithoutDate { get; }
        public IReadOnlyList<string> WithMultipleDate { get; }
    }
}