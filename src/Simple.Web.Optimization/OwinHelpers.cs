namespace Simple.Web.Optimization
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Owin;

    public static class OwinHelpers
    {
        public static void UseSimpleOptimization(this IAppBuilder app)
        {
            app.Use(new Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>(next =>
            {
                var middleWare = new BundleMiddleWare(next);
                return (Func<IDictionary<string, object>, Task>) middleWare.Handle;
            }));
        }
    }
}