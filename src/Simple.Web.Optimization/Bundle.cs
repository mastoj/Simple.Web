using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Simple.Web.Optimization
{
    public abstract class Bundle : IBundle
    {
        protected abstract string Template { get; }
        private readonly string _bundleName;
        private BundleDefinition _bundleDefinition;

        protected Bundle(string bundleName)
        {
            _bundleName = bundleName;
            _bundleDefinition = new BundleDefinition();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals(obj as Bundle);
        }

        protected bool Equals(Bundle other)
        {
            return string.Equals(_bundleName, other._bundleName);
        }

        public override int GetHashCode()
        {
            return (_bundleName != null ? _bundleName.GetHashCode() : 0);
        }

        public string BundlePath { get { return _bundleName; } }

        public BundleDefinition BundleDefinition
        {
            get { return _bundleDefinition; }
        }

        public IEnumerable<FileInfo> GetFiles()
        {
            return _bundleDefinition.GetFiles();
        }

        public string Render()
        {
            var output = new StringBuilder();
            foreach (var fileInfo in GetFiles())
            {
                var serverPath = PathHelper.GetServerPath(fileInfo.FullName);
                output.AppendLine(string.Format(Template, serverPath));
            }
            return output.ToString();
        }
    }
}