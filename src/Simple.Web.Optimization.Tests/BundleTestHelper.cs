namespace Simple.Web.Optimization.Tests
{
    public static class BundleTestHelper
    {
        public static BundleDefinition CreateDefinition(string bundlePath)
        {
            Bundler.Reset();
            var bundle = new ScriptBundle(bundlePath);
            return Bundler.Add(bundle);
        }
    }
}