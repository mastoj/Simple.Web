using System.Collections;

namespace Simple.Web.Optimization
{
    public static class Bundler
    {
        private static BundleCollection _bundleCollection = new BundleCollection();
        public static BundleDefinition Add(IBundle bundle)
        {
            return _bundleCollection.Add(bundle);
        }

        public static void Reset()
        {
            _bundleCollection = new BundleCollection();
        }

        public static IBundle GetBundle(string bundlePath)
        {
            return _bundleCollection.GetBundle(bundlePath);
        }

        public static string GetBundleContent(string path)
        {
            if (path == "/hello")
            {
                return "Hello world!";                
            }
            return null;
        }
    }
}
