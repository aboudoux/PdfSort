using System;
using System.Collections.Generic;

namespace SortExpenses
{
    public static class ConsoleExtentions
    {
        public static bool IsEmpty(this string[] args)
        {
            return args.Length == 0;
        }

        public static void ForEach(this IEnumerable<string> enumerable, Action<string> action)
        {
            foreach (var en in enumerable)
                action(en);
        }
    }
}