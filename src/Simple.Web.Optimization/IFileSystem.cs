namespace Simple.Web.Optimization
{
    using System.Collections.Generic;

    public interface IFileSystem
    {
        bool ItemExists(string path);
        string GetFullPath(string path);
        string GetContent(string path);
        bool IsFile(string path);
        IEnumerable<string> GetFiles(string folderPath);
    }
}