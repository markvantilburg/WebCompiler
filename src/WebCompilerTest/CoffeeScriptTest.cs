﻿using System;
using System.IO;
using System.Linq;
using WebCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCompilerTest
{
    [TestClass]
    public class CoffeeScriptTest
    {
        private ConfigFileProcessor _processor;

        [TestInitialize]
        public void Setup()
        {
            _processor = new ConfigFileProcessor();
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("../../artifacts/coffee/test.js");
            File.Delete("../../artifacts/coffee/test.min.js");
        }

        [TestMethod, TestCategory("CoffeeScript")]
        public void CompileCoffeeScript()
        {
            var result = _processor.Process("../../artifacts/coffeeconfig.json");
            Assert.IsTrue(File.Exists("../../artifacts/coffee/test.js"));
        }

        [TestMethod, TestCategory("CoffeeScript")]
        public void CompileCoffeeScriptWithError()
        {
            var result = _processor.Process("../../artifacts/coffeeconfigerror.json");
            var error = result.ElementAt(0).Errors[0];
            Assert.AreEqual(2, error.LineNumber);
            Assert.AreEqual("Unexpected '}'", error.Message);
        }
    }
}
