namespace Simple.Web.Optimization
{
    public static class BundlerRenderer
    {
        public static string Render(string bundlePath)
        {
            var bundle = Bundler.GetBundle(bundlePath);
            return bundle.Render();
        }
    }
}