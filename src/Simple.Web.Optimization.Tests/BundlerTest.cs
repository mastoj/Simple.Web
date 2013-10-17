using System.IO;
using System.Linq;
using Xunit;

namespace Simple.Web.Optimization.Tests
{
    public class BundlerTests
    {
        [Fact]
        public void AddScriptBundlerShouldReturnBundlerDefinition()
        {
            var bundle = new ScriptBundle("assets/js");
            var actual = Bundler.Add(bundle);

            Assert.IsType<BundleDefinition>(actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void AddScriptBundleWithSameNameTwiceShouldSameDefinition()
        {
            var bundle = new ScriptBundle("assets/js");
            var bundle2 = new ScriptBundle("assets/js");
            var expected = Bundler.Add(bundle);
            var actual = Bundler.Add(bundle2);

            Assert.Same(expected, actual);
        }
    }

    public class BundlerDefinitionTests
    {
        private string _bundlePath = "assets/js";
        private IBundle _bundle;

        [Fact]
        public void AddOneReturnsSameDefinitionWithOneFile()
        {
            var definition = BundleTestHelper.CreateDefinition(_bundlePath);
            var actual = definition.Include("assets/js/file1.js");
            Assert.Same(definition, actual);

            var files = Bundler.GetBundle(_bundlePath).GetFiles().ToList();
            Assert.Equal(1, files.Count);
        }

        [Fact]
        public void AddingNonExistingFileThrowsFileNotFound()
        {
            var definition = BundleTestHelper.CreateDefinition(_bundlePath);
            Assert.Throws<FileNotFoundException>(() => definition.Include("assets/js/fileNotFound.js"));
        }

        [Fact]
        public void AddingTwoExistingFilesShouldBeInTheRightOrderWhenGettingThem()
        {
            var definition = BundleTestHelper.CreateDefinition(_bundlePath);
            var actual = definition.Include("assets/js/file1.js", "assets/js/file2.js");
            Assert.Same(definition, actual);

            var files = Bundler.GetBundle(_bundlePath).GetFiles().ToList();
            Assert.Equal(2, files.Count);
        }
    }
}
