using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SortExpenses.ExpensesReaders;
using SortExpenses.Folders;
using SortExpenses.Sorting.Strategies;

namespace SortExpenses
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = Arguments.Parse(args);

                Console.WriteLine("Extracting files (please wait)");
                var files = Sorting.SortExpenses
                    .WithReader(new SimpleExpensesReader())
                    .ForFolder(new Folder(arguments.Folder));

                var getFiles = arguments.TakeFirstDateIfMultipleDate
                    ? files.With<MergeSingleDateAndMultipleDate>()
                    : files.With<SortByDate>();

                WriteAll(getFiles.SortedByDates.Select(a=>a.fileName).ToList(), "Liste des fichiers correctements triés", ConsoleColor.Green);
                WriteAll(getFiles.WithMultipleDate, "Liste des fichiers dont plusieurs dates ont été trouvées", ConsoleColor.Yellow);
                WriteAll(getFiles.WithoutDate, "Liste des fichiers dont la date n'est pas trouvée", ConsoleColor.Red);
                
                if (arguments.RenameFiles)
                    Rename(getFiles.SortedByDates);
            }
            catch (ArgumentsException e)
            {
                Console.WriteLine(e.Message);
                PrintUsage();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + Environment.NewLine + "-------------" + Environment.NewLine);
                Console.WriteLine(e.Message);
            }
        }

        public static void Rename(IReadOnlyList<(string fileName, DateTime retainedDate)> files)
        {
            var i = 1;
            files.ForEach(file => File.Move(file.fileName, Path.Combine(Path.GetDirectoryName(file.fileName), $"{file.retainedDate:yyyy-MM-dd}" + "_" + i++ + Path.GetExtension(file.fileName))));
        }

        private static void WriteAll(IReadOnlyList<string> list, string title, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(title);            
            Console.WriteLine("--------------------------------");
            list.ForEach(Console.WriteLine);
            Console.WriteLine("--------------------------------");
            Console.ResetColor();
        }

        private static void PrintUsage()
        {
            Console.WriteLine(
                $"Sort PDF files by date, version {Assembly.GetEntryAssembly().GetName().Version}" + Environment.NewLine + Environment.NewLine +
                "Usage : SortExpenses.exe -f [folder] (options)" + Environment.NewLine +
                "options are : " + Environment.NewLine +
                "\t-r : Rename all files ordered by date" + Environment.NewLine +
                "\t-i : Take first date if multiple date are found" + Environment.NewLine
                );
            Environment.Exit(0);
        }
    }
}
