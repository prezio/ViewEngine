﻿using System;
using System.IO;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewEngine.Core.Grammar;
using FluentAssertions;
using ViewEngine.Core.Engine;
using ViewEngine.Core.Grammar.Model;

namespace ViewEngine.UnitTests
{
    [TestClass]
    public class ViewEngineVisitorTests
    {
        private ViewEngineVisitor _testVisitor;
        private ViewEngineParser CreateTestParser(string content)
        {
            var inputStream = new AntlrInputStream(new StringReader(content));
            var viewEngineLexer = new ViewEngineLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(viewEngineLexer);
            return new ViewEngineParser(commonTokenStream);
        }
        
        [TestInitialize]
        public void TestInitialize()
        {
            _testVisitor = new ViewEngineVisitor();
        }

        #region Model Visit Tests
        [TestMethod]
        public void ViewEngineVisitor_should_properly_recognize_Model_declaration()
        {
            // GIVEN
            var testType = "TestType";
            var parser = CreateTestParser($"model @{{{testType}}}");
            
            // WHEN
            _testVisitor.Visit(parser.model_introduction());

            // THEN
            _testVisitor.Model.Should().NotBeNull();
            _testVisitor.Model.VarType.Should().BeEquivalentTo(testType);
        }

        [TestMethod]
        public void ViewEngineVisitor_should_fail_when_Model_declaration_has_no_type()
        {
            // GIVEN
            var parser = CreateTestParser("model");

            // WHEN
            Action action = () => { _testVisitor.Visit(parser.model_introduction()); };

            // THEN
            Assert.ThrowsException<RenderException>(action);
        }

        [TestMethod]
        public void ViewEngineVisitor_should_fail_when_Model_is_declared_but_it_already_exists()
        {
            // GIVEN
            var parser = CreateTestParser(@"model @{NewTestType}");
            _testVisitor.Model = new ModelIntroduceExpression("TestType");

            // WHEN
            Action action = () => { _testVisitor.Visit(parser.model_introduction()); };

            // THEN
            Assert.ThrowsException<RenderException>(action);
        }
        #endregion

        #region Mixin Usage Tests

        #endregion
    }
}
