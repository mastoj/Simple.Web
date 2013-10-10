namespace Simple.Web.Optimization
{
    public static class BundlerSettings
    {
        private static bool _optimize = true;

        public static bool Optimize
        {
            get
            {
                return _optimize;
            }
            set
            {
                _optimize = value;
            }
        }
    }
}