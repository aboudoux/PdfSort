using System;
using System.IO;
using System.Linq;
using Fclp;
using FluentAssertions;
using NUnit.Framework;

namespace SortExpenses.Tests
{
    public class FluentCommandLineTests
    {
        [Test]
        public void SetupWithSimpleArgument()
        {
            var args = new[] {"-d", "c:\\temp", "-r"};

            var f = new FluentCommandLineParser<TestArguments>();

            f.Setup(a => a.Directory)
                .As('d')
                .WithDescription("Directory to scan")
                .Required();

            f.Setup(a => a.RenameFiles)
                .As('r')
                .WithDescription("Rename files that matches");

            f.SetupHelp("h", "?");

            var result = f.Parse(args);
          
            
            f.Object.Directory.Should().Be("c:\\temp");
            f.Object.RenameFiles.Should().BeTrue();
        }

        [Test]
        public void UseArgumentsConsole()
        {
            var args = new[] { "-f", "c:\\temp", "-r" };
            var arguments =  Arguments.Parse(args);
            arguments.Should().NotBeNull();

            arguments.Folder.Should().Be("c:\\temp");
            arguments.RenameFiles.Should().BeTrue();
        }
    }

    public class TestArguments
    {
        private string _directory;

        public string Directory
        {
            get => _directory;
            set => _directory = CheckDirectoryExists(value);
        }

        private static string CheckDirectoryExists(string directory)
        {
            if(!System.IO.Directory.Exists(directory))
                throw new DirectoryNotFoundException();
            return directory;
        }

        public bool RenameFiles { get; set; }
    }
}