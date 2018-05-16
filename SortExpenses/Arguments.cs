using System;
using Fclp;

namespace SortExpenses
{
    public class Arguments
    {
        public static Arguments Parse(string[] args)
        {
            var parser = new FluentCommandLineParser<Arguments>();

            parser.Setup(a => a.Folder)
                .As('f')
                .Required();

            parser.Setup(a => a.RenameFiles)
                .As('r');

            parser.Setup(a => a.TakeFirstDateIfMultipleDate)
                .As('i');

            parser.SetupHelp("h", "?");

            var result = parser.Parse(args);
            if(result.HasErrors)
                throw new ArgumentsException(result.ErrorText);

            return parser.Object;
        }

        public string Folder { get; private set; }
        public bool RenameFiles { get; private set; }

        public bool TakeFirstDateIfMultipleDate { get; private set; }
    }

    public class ArgumentsException : Exception
    {
        public ArgumentsException(string message) : base(message)
        {
            
        }
    }
}