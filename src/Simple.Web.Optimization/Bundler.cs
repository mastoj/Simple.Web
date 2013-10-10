using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Simple.Optimization
{
    public static class Bundler
    {
        private static BundleCollection _bundleCollection = new BundleCollection();
        public static BundleDefinition Add(IBundle bundle)
        {
            return _bundleCollection.Add(bundle);
        }

        public static IEnumerable<FileInfo> GetFiles(string bundlePath)
        {
            return _bundleCollection.GetFiles(bundlePath);
        }

        public static void Reset()
        {
            _bundleCollection = new BundleCollection();
        }
    }

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
            var pattern = @"(\\|/)bin(\\|/)?([Dd]ebug|[Rr]elease)?$";
            var target = Path.GetDirectoryName(typePath);
            return new Regex(pattern).Replace(target, string.Empty);
        }

        private static string GetPath(this Assembly assembly)
        {
            return new Uri(assembly.EscapedCodeBase).LocalPath;
        }
    }

    internal class BundleCollection
    {
        private Dictionary<IBundle, BundleDefinition> _bundleDefinitions = new Dictionary<IBundle, BundleDefinition>();

        internal BundleDefinition Add(IBundle bundle)
        {
            if (!_bundleDefinitions.ContainsKey(bundle))
            {
                _bundleDefinitions.Add(bundle, new BundleDefinition());
            }
            return _bundleDefinitions[bundle];
        }

        public IEnumerable<FileInfo> GetFiles(string bundlePath)
        {
            var definition = _bundleDefinitions.Single(y => y.Key.BundlePath == bundlePath).Value;
            return definition.GetFiles();
        }
    }

    public class ScriptBundle : Bundle
    {
        public ScriptBundle(string bundleName) : base(bundleName)
        {
        }
    }

    public class BundleDefinition
    {
        private Collection<FileInfo> _fileInfos = new Collection<FileInfo>();

        public BundleDefinition Include(params string[] paths)
        {
            foreach (var path in paths)
            {
                var qualifiedPath = PathHelper.GetPath(path);
                _fileInfos.Add(new FileInfo(qualifiedPath));                
            }
            return this;
        }

        public IEnumerable<FileInfo> GetFiles()
        {
            return _fileInfos;
        }
    }

    public interface IBundle
    {
        string BundlePath { get; }
    }

    public abstract class Bundle : IBundle
    {
        private readonly string _bundleName;

        protected Bundle(string bundleName)
        {
            _bundleName = bundleName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals(obj as Bundle);
        }

        protected bool Equals(Bundle other)
        {
            return string.Equals(_bundleName, other._bundleName);
        }

        public override int GetHashCode()
        {
            return (_bundleName != null ? _bundleName.GetHashCode() : 0);
        }

        public string BundlePath { get { return _bundleName; } }
    }

    public static class BundlerSettings
    {
        private static bool _optimize = true;

        public static bool Optimize
        {
            get
            {
                return _optimize;
            }
            set
            {
                _optimize = value;
            }
        }
    }

    public static class BundlerRenderer
    {
        public static void Render(string bundlePath)
        {
            var files = Bundler.GetFiles(bundlePath);
        }
    }
}
