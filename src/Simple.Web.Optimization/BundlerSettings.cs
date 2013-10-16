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

        public static IFileSystem _fileSystem;

        public static IFileSystem FileSystem
        {
            get
            {
                _fileSystem = _fileSystem ?? new FileSystem();
                return _fileSystem;
            }
            set
            {
                _fileSystem = value;
            }
        }
    }
}