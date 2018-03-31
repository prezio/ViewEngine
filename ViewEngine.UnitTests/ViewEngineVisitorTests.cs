using System;
using System.IO;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewEngine.Core.Grammar;
using FluentAssertions;
using ViewEngine.Core.Engine;
using ViewEngine.Core.Grammar.Model;
using System.Linq;
using ViewEngine.Core.Grammar.Common;

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
        [TestMethod]
        public void ViewEnigineVisitor_should_visit_parameterless_mixin_usage_properly()
        {
            // GIVEN
            var methodName = "TestMethod";
            var parser = CreateTestParser($"{methodName}()");

            // WHEN
            var mixinExp = (MixinUsageExpression)_testVisitor.Visit(parser.mixin_usage());

            // THEN
            mixinExp.MixinName.Should().BeEquivalentTo(methodName);
            mixinExp.VariableAssignments.Should().BeEmpty();
        }

        [TestMethod]
        public void ViewEngineVisitor_should_visit_mixin_usage_with_basic_parameters_properly()
        {
            // GIVEN
            var methodName = "TestMethod";
            var testParam1 = "TestParam1";
            var testMsg1 = "Hello World 1";

            var testParam2 = "TestParam2";
            var testMsg2 = "Hello World 2";

            var exp = $"{methodName} ({testParam1}=\"{testMsg1}\", {testParam2}=\"{testMsg2}\")";
            var parser = 
                CreateTestParser(exp);

            // WHEN
            var mixinExp = (MixinUsageExpression)_testVisitor.Visit(parser.mixin_usage());

            // THEN
            mixinExp.MixinName.Should().BeEquivalentTo(methodName);
            Assert.AreEqual(mixinExp.VariableAssignments.Count, 2);
            Assert.IsTrue(mixinExp.VariableAssignments.ContainsKey(testParam1));

            var value1 = (TextString)mixinExp.VariableAssignments[testParam1];
            value1.Text.Should().BeEquivalentTo(testMsg1);

            var value2 = (TextString)mixinExp.VariableAssignments[testParam2];
            value2.Text.Should().BeEquivalentTo(testMsg2);
        }
        #endregion
    }
}
