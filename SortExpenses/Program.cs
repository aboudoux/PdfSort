using System;
using System.IO;
using System.Reflection;
using Fclp;
using SortExpenses.ExpensesReaders;
using SortExpenses.Folders;

namespace SortExpenses
{
    class Program
    {
        static void Main(string[] args)
        {

            /*var parser = new FluentCommandLineParser<Arguments>();
            parser.Setup(arg => arg.Folder)
                .As('f')
*/
            try
            {
                var sort = new SortExpenses(new IronExpensesReader());
                var folder = new Folder(args[0]);

                var i = 1;
                sort.ByDate(folder).SortedByDates.ForEach(file => File.Move(file, $"{i++}_{file}"));
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

    public class Arguments
    {
        public string Folder { get; set; }
        public bool RenameFiles { get; set; }
    }
}
