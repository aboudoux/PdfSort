using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PdfSort.Extractions
{
    public abstract class DataExtractor<T>
    {
        protected abstract string RegexPattern { get; }
        protected abstract T Parse(string data);

        public IReadOnlyList<T> Extract(string content)
        {
            var result = new List<T>();
            foreach (var element in Regex.Matches(content, RegexPattern))
                result.Add(Parse(element.ToString()));
            return result;
        }
    }
}