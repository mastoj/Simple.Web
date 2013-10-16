using System.Collections.Generic;
using System.IO;

namespace Simple.Web.Optimization
{
    using System.Threading.Tasks;

    public interface IBundle
    {
        string BundlePath { get; }
        BundleDefinition BundleDefinition { get; }
        IEnumerable<FileInfo> GetFiles();
        Task WriteOutput(IDictionary<string, object> env);
        string Render();
    }
}