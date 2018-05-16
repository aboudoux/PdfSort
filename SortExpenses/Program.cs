using System;
using System.IO;
using System.Reflection;
using SortExpenses.ExpensesReaders;
using SortExpenses.Folders;

namespace SortExpenses
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = Arguments.Parse(args);
                var sort = new SortExpenses(new IronExpensesReader());
                var folder = new Folder(arguments.Folder);

                Console.WriteLine("Extracting files (please wait)");
                var getFiles = sort.ByDate(folder);

                Console.WriteLine("Liste des fichiers correctements tirés");
                Console.WriteLine("--------------------------------");
                getFiles.SortedByDates.ForEach(Console.WriteLine);
                Console.WriteLine("--------------------------------");

                Console.WriteLine("Liste des fichiers dont la date n'est pas trouvées");
                Console.WriteLine("--------------------------------");
                getFiles.WithoutDate.ForEach(Console.WriteLine);
                Console.WriteLine("--------------------------------");


                if (arguments.RenameFiles)
                {
                    var i = 1;
                    getFiles.SortedByDates.ForEach(file => File.Move(file, $"{i++}_{file}"));
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

        private static void PrintUsage()
        {
            Console.WriteLine(
                $"Sort PDF files by date, version {Assembly.GetEntryAssembly().GetName().Version}" + Environment.NewLine + Environment.NewLine +
                "Usage : SortExpenses.exe [folder]" + Environment.NewLine
                );
            Environment.Exit(0);
        }
    }
}
