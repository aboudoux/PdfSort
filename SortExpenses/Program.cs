using System;
using System.Collections.Generic;
using System.IO;
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
                var getFiles = Sorting.SortExpenses
                                .WithReader(new SimpleExpensesReader())
                                .ForFolder(new Folder(arguments.Folder))
                                .By(new SortByDate());

                WriteAll(getFiles.SortedByDates, "Liste des fichiers correctements triés", ConsoleColor.Green);
                WriteAll(getFiles.WithMultipleDate, "Liste des fichiers dont plusieurs dates ont été trouvées", ConsoleColor.Yellow);
                WriteAll(getFiles.WithoutDate, "Liste des fichiers dont la date n'est pas trouvée", ConsoleColor.Red);
                
                if (arguments.RenameFiles)
                {
                    var i = 1;
                    getFiles.SortedByDates.ForEach(file => File.Move(file, Path.Combine(Path.GetDirectoryName(file),"Sorted_" + i++ + "_" + Path.GetFileName(file))));
                }
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
                "\t-r : Rename all files ordered by date"
                );
            Environment.Exit(0);
        }
    }
}
