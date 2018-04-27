using System;

namespace SortExpenses.Extractions
{
    public class ExtractDate_ddMMyySlashes : DataExtractor<DateTime>
    {
        protected override string RegexPattern => @"\d{2}/\d{2}/\d{2}\b";
        protected override DateTime Parse(string data) => data.ToDate(DateExtentions.ddMMyySlashes);
    }
}