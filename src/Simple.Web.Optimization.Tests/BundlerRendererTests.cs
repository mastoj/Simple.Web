using System.Linq;
using Xunit;

namespace Simple.Web.Optimization.Tests
{
    public class BundlerRendererTests
    {
        private IBundle _bundle;
        private string _bundlePath = "assets/js";

        public BundlerRendererTests()
        {
            BundlerSettings.Optimize = false;
        }

        [Fact]
        public void RenderOneScriptWithNoOptimizationAndOneFileGiveTag()
        {
            var definition = BundleTestHelper.CreateDefinition(_bundlePath);
            definition.Include("assets/js/file1.js");
            BundlerSettings.Optimize = false;
            var actual = BundlerRenderer.Render(_bundlePath).Trim();

            Assert.Equal("<script type='text/javascript' src='/assets/js/file1.js' />", actual);
        }

        [Fact]
        public void RenderTwoScriptsWithNoOptimizationAndOneFileGivesTwoTag()
        {
            var definition = BundleTestHelper.CreateDefinition(_bundlePath);
            definition.Include("assets/js/file1.js");
            definition.Include("assets/js/file2.js");
            BundlerSettings.Optimize = false;
            var actual = BundlerRenderer.Render(_bundlePath).Split(new [] { '\r', '\n' }).Select(y => y.Trim()).Where(y => !string.IsNullOrEmpty(y)).ToList();

            Assert.Equal(2, actual.Count);
            Assert.Equal("<script type='text/javascript' src='/assets/js/file1.js' />", actual[0]);
            Assert.Equal("<script type='text/javascript' src='/assets/js/file2.js' />", actual[1]);
        }


    }
}