using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SortExpenses.Extractions
{
    public abstract class DataExtractor<T>
    {
        protected abstract string RegexPattern { get; }
        protected abstract T Parse(string data);

        public IReadOnlyList<(string source, T parsedData)> Extract(string content)
        {
            var result = new List<(string source, T parsedData)>();
            foreach (var element in Regex.Matches(content, RegexPattern))
            {
                try
                {
                    result.Add((element.ToString(),Parse(element.ToString())));
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }
    }
}