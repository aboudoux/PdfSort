using System;

namespace PdfSort.Extractions
{
    public class ExtractDate_ddMMyyDashes : DataExtractor<DateTime>
    {
        protected override string RegexPattern => @"\d{2}-\d{2}-\d{2}";
        protected override DateTime Parse(string data) => data.ToDate(DateExtentions.ddMMyyDashes);
    }
}