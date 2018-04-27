using System;
using System.IO;

namespace SortExpenses.Folders
{
    public class Folder : IFolder
    {
        private readonly string _folderPath;

        public Folder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new Exception($"Folder {folderPath} does not exists");

            _folderPath = folderPath;
        }
        
        public string[] GetPdfFiles()
        {
            return Directory.GetFiles(_folderPath, "*.pdf");
        }
    }
}