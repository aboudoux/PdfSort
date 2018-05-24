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

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var en in enumerable)
                action(en);
        }
    }
}