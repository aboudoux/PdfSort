﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SortExpenses.Extractions
{
    public class ExtractDate_ddMMMyyyyDashes : DataExtractor<DateTime>
    {
        private readonly Dictionary<string, string> _monthMapping = new Dictionary<string, string>()
        {
            {"JAN", "Jan"},
            {"FEV", "Feb"},
            {"MARS", "Mar"},
            {"AVR", "Apr"},
            {"MAI", "May"},
            {"MRI", "May"},
            {"JUIN", "Jun"},
            {"JUIL", "Jul"},
            {"AOUT", "Aug"},
            {"SEPT", "Sep"},
            {"OCT", "Oct"},
            {"NOV", "Nov"},
            {"DEC", "Dec"},
        };

        protected override string RegexPattern => @"\d{2}-\w{3}-20\d{2}";

        protected override DateTime Parse(string data)
        {
           return Normalize(data.ToUpper()).ToDate(DateExtentions.ddMMMyyyyDashes);
        }

        private string Normalize(string format) => _monthMapping.Aggregate(format, (current, pair) => current.Replace(pair.Key, pair.Value));
    }
}