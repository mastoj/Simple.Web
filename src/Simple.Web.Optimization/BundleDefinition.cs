using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Simple.Web.Optimization
{
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
}