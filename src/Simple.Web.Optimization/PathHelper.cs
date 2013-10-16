using System;
using System.IO;
using System.Reflection;

namespace Simple.Web.Optimization
{
    internal static class PathHelper
    {
        private static string AppRoot = AssemblyAppRoot(typeof (PathHelper).Assembly.GetPath());

        public static string GetPath(string filePath)
        {
            if (File.Exists(filePath))
            {
                return filePath;
            }

            filePath = Path.Combine(AppRoot, filePath);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            return filePath;
        }
        
        private static string AssemblyAppRoot(string typePath)
        {
            const string pattern = @"(\\|/)bin(\\|/)?([Dd]ebug|[Rr]elease)?$";
            var target = Path.GetDirectoryName(typePath);
            return target.RegexReplace(pattern, string.Empty);
        }

        private static string GetPath(this Assembly assembly)
        {
            return new Uri(assembly.EscapedCodeBase).LocalPath;
        }

        public static string GetServerPath(string filePath)
        {
            var serverPath = filePath.Replace(AppRoot, string.Empty).RegexReplace(@"\\", "/");
            return serverPath;
        }
    }
}