using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Xunit;

namespace Simple.Optimization.Tests
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
            var definition = CreateDefinition();
            var actual = definition.Include("assets/js/file1.js");
            Assert.Same(definition, actual);

            var files = Bundler.GetFiles(_bundlePath).ToList();
            Assert.Equal(1, files.Count);
            Assert.Equal("file1.js", files[0].Name);
        }

        [Fact]
        public void AddingNonExistingFileThrowsFileNotFound()
        {
            var definition = CreateDefinition();
            Assert.Throws<FileNotFoundException>(() => definition.Include("assets/js/fileNotFound.js"));
        }

        [Fact]
        public void AddingTwoExistingFilesShouldBeInTheRightOrderWhenGettingThem()
        {
            var definition = CreateDefinition();
            var actual = definition.Include("assets/js/file1.js", "assets/js/file2.js");
            Assert.Same(definition, actual);

            var files = Bundler.GetFiles(_bundlePath).ToList();
            Assert.Equal(2, files.Count);
            Assert.Equal("file1.js", files[0].Name);
            Assert.Equal("file2.js", files[1].Name);
        }

        private BundleDefinition CreateDefinition()
        {
            Bundler.Reset();
            _bundle = new ScriptBundle(_bundlePath);
            return Bundler.Add(_bundle);
        }
    }

    public class BundlerRendererTests
    {
        private IBundle _bundle;
        private BundleDefinition _definition;
        private string _bundlePath = "assets/js";


        public BundlerRendererTests()
        {
            _bundle = new ScriptBundle("assets/js");
            _definition = Bundler.Add(_bundle);
            BundlerSettings.Optimize = false;
        }

        [Fact]
        public void RenderOneScriptWithNoOptimizationAndOneFileGiveTag()
        {
            BundlerRenderer.Render(_bundlePath);
        }
    }
}
