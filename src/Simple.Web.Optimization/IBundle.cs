using System.Collections.Generic;
using System.IO;

namespace Simple.Web.Optimization
{
    public interface IBundle
    {
        string BundlePath { get; }
        BundleDefinition BundleDefinition { get; }
        IEnumerable<FileInfo> GetFiles();
        string Render();
    }
}