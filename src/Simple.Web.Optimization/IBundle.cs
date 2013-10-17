using System.Collections.Generic;

namespace Simple.Web.Optimization
{
    using System.Threading.Tasks;

    public interface IBundle
    {
        string BundlePath { get; }
        BundleDefinition BundleDefinition { get; }
        IEnumerable<string> GetFiles();
        Task WriteOutput(IDictionary<string, object> env);
        string Render();
    }
}