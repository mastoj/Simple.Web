using System.Linq;

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
            return PathHelper.GetPath(path);
        }

        public string GetContent(string path)
        {
            if (IsFile(path))
            {
                using (var streamReader = new StreamReader(new FileInfo(path).OpenRead()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            throw new ArgumentException("Path is not a file: " + path, "path");
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
            var directoryInfo = new DirectoryInfo(GetFullPath(folderPath));
            if (directoryInfo.Exists)
            {
                var files = directoryInfo.GetFiles().Select(y => y.FullName);
                return files;
            }
            throw new ArgumentException("Folder path is not a directory: " + folderPath, "folderPath");
        }
    }
}