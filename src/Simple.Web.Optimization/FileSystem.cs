namespace Simple.Web.Optimization
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FileSystem : IFileSystem
    {
        public bool ItemExists(string path)
        {
            var file = new FileInfo(GetFullPath(path));
            if (file.Exists)
                return true;
            var directory = new DirectoryInfo(GetFullPath(path));
            return directory.Exists;
        }

        public string GetFullPath(string path)
        {
            throw new System.NotImplementedException();
        }

        public string GetContent(string path)
        {
            throw new System.NotImplementedException();

        }

        public bool IsFile(string path)
        {
            var fileInfo = new FileInfo(GetFullPath(path));
            if (fileInfo.Exists)
            {
                return fileInfo.Exists;                
            }
            throw new ArgumentException("Path is not a file: " + path, "path");
        }

        public IEnumerable<string> GetFiles(string folderPath)
        {
            throw new System.NotImplementedException();
        }
    }
}