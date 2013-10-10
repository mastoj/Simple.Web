using System.Collections.ObjectModel;
using System.Linq;

namespace Simple.Web.Optimization
{
    internal class BundleCollection
    {
        private Collection<IBundle> _bundles = new Collection<IBundle>();

        internal BundleDefinition Add(IBundle bundle)
        {
            var existingBundle = _bundles.FirstOrDefault(y => y.BundlePath == bundle.BundlePath);
            if (existingBundle == null)
            {
                _bundles.Add(bundle);
                return bundle.BundleDefinition;
            }
            return existingBundle.BundleDefinition;
        }

        public IBundle GetBundle(string bundlePath)
        {
            return _bundles.Single(y => y.BundlePath == bundlePath);
        }
    }
}