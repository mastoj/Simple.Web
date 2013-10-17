using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Simple.Web.Optimization
{
    public class BundleDefinition
    {
        private Collection<string> _itemPaths = new Collection<string>();

        public BundleDefinition Include(params string[] paths)
        {
            foreach (var path in paths)
            {
                var qualifiedPath = BundlerSettings.FileSystem.GetFullPath(path);
                _itemPaths.Add(qualifiedPath);                
            }
            return this;
        }

        public IEnumerable<string> GetFiles()
        {
            return _itemPaths;
        }
    }
}