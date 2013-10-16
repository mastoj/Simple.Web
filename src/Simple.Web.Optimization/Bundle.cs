namespace Simple.Web.Optimization
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Bundle : IBundle
    {
        public const string ResponseBody = "owin.ResponseBody";
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

        public Task WriteOutput(IDictionary<string, object> env)
        {
            var responseText = GetBundleContent();
            if (responseText != null)
            {
                WriteResponse(env, responseText);
                var tcs = new TaskCompletionSource<object>();
                tcs.SetResult(responseText);
                return tcs.Task;
            }
            return null;
        }

        private static void WriteResponse(IDictionary<string, object> env, string responseText)
        {
            //var requestHeader = (IDictionary<string, string[]>)env["owin.RequestHeaders"];
            //var responseHeader = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];

            //responseHeader.Add("Content-Type", requestHeader["Accept"]);
            env["owin.ResponseStatusCode"] = 200;
            Stream outStream = env[ResponseBody] as Stream;
            using (var writeStream = new StreamWriter(outStream))
            {
                writeStream.Write(responseText);
            }
        }

        private string GetBundleContent()
        {
            return "Hello world!";
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