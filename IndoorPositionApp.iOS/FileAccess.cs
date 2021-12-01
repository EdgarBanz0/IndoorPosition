using System;
using System.IO;

namespace IndoorPositionApp.iOS
{
    class FileAccess
    {
        public static string GetLocalFilePath(string filename)
        {
            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, filename);
            return path;
        }
    }
}