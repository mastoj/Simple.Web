namespace Simple.Web.Optimization
{
    public class ScriptBundle : Bundle
    {
        private const string template = "<script type='text/javascript' src='{0}' />";

        public ScriptBundle(string bundleName) : base(bundleName)
        {
        }

        protected override string Template
        {
            get { return template; }
        }
    }
}