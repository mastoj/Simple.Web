namespace Simple.Web.Optimization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class BundleMiddleWare
    {
        private Func<IDictionary<string, object>, Task> _next;

        public BundleMiddleWare(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public const string Path = "owin.RequestPath";
        public Task Handle(IDictionary<string, object> env)
        {
            var path = GetPath(env);
            var bundle = Bundler.GetBundle(path);
            if (bundle != null)
            {
                var task = bundle.WriteOutput(env);
                return task;                
            }
            return _next(env);
        }

        private static string GetPath(IDictionary<string, object> env)
        {
            if (env.ContainsKey(Path))
            {
                return env[Path] as string;
            }
            return null;
        }
    }
}