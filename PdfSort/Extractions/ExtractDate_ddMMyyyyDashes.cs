using System;

namespace SortExpenses.Extractions
{
    public class ExtractDate_ddMMyyyyDashes : DataExtractor<DateTime>
    {
        protected override string RegexPattern => @"\d{2}-\d{2}-20\d{2}";
        protected override DateTime Parse(string data) => data.ToDate(DateExtentions.ddMMyyyyDashes);
    }
}