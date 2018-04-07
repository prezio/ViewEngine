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
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Grammar.Instructions;

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
            var mixinName = "TestMethod";
            var parser = CreateTestParser($"{mixinName}()");

            // WHEN
            var mixinExp = (MixinUsageExpression)_testVisitor.Visit(parser.mixin_usage());

            // THEN
            mixinExp.MixinName.Should().BeEquivalentTo(mixinName);
            mixinExp.VariableContents.Should().BeEmpty();
        }

        [TestMethod]
        public void ViewEngineVisitor_should_visit_mixin_usage_with_basic_parameters_properly()
        {
            // GIVEN
            var mixinName = "TestMethod";
            var testMsg1 = "Hello World 1";
            var testMsg2 = "Hello World 2";

            var exp = $"{mixinName} (\"{testMsg1}\",\"{testMsg2}\")";
            var parser = CreateTestParser(exp);

            // WHEN
            var mixinExp = (MixinUsageExpression)_testVisitor.Visit(parser.mixin_usage());

            // THEN
            mixinExp.MixinName.Should().BeEquivalentTo(mixinName);
            Assert.AreEqual(mixinExp.VariableContents.Count, 2);
            
            var value1 = (TextString)mixinExp.VariableContents[0];
            value1.Text.Should().BeEquivalentTo(testMsg1);

            var value2 = (TextString)mixinExp.VariableContents[1];
            value2.Text.Should().BeEquivalentTo(testMsg2);
        }

        [TestMethod]
        public void ViewEngineVisitor_should_visit_mixin_usage_with_complex_regular_parameter_properly()
        {
            // GIVEN
            var mixinName = "methodName";
            var methodTest = "methodTest";

            var exp = $"{mixinName}(<| {methodTest}() |>)";
            var parser = CreateTestParser(exp);

            // WHEN
            var mixinExp = (MixinUsageExpression)_testVisitor.Visit(parser.mixin_usage());

            // THEN
            mixinExp.MixinName.Should().BeEquivalentTo(mixinName);
            Assert.AreEqual(mixinExp.VariableContents.Count, 1);

            var content = (RegularScope)mixinExp.VariableContents.First();
            Assert.AreEqual(content.Result.Count, 1);
            var insideMixin = (MixinUsageExpression)content.Result.First();
            Assert.AreEqual(insideMixin.MixinName, methodTest);
            insideMixin.VariableContents.Should().BeEmpty();
        }

        [TestMethod]
        public void ViewEngineVisitor_should_visit_mixin_usage_with_complex_template_parameter_properly()
        {
            // GIVEN
            var mixinName = "mixinName";

            var exp = $"{mixinName} (<- Sample template ->)";
            var parser = CreateTestParser(exp);

            // WHEN
            var mixinExp = (MixinUsageExpression)_testVisitor.Visit(parser.mixin_usage());

            // THEN
            mixinExp.MixinName.Should().BeEquivalentTo(mixinName);
            Assert.AreEqual(mixinExp.VariableContents.Count, 1);
            Assert.IsTrue(mixinExp.VariableContents.First() is TemplateScope);
        }
        #endregion

        #region Mixin Declaration Tests
        [TestMethod]
        public void ViewEngineVisitor_should_visit_mixin_declaration_with_regular_body_properly()
        {
            // GIVEN
            var mixinName = "mixinName";

            var exp = $"mixin {mixinName} () <| SampleTest() |>";
            var parser = CreateTestParser(exp);

            // WHEN
            _testVisitor.Visit(parser.mixin_declaration());

            // THEN
            Assert.AreEqual(_testVisitor.Mixins.Count, 1);
            var mixin = _testVisitor.Mixins.First();
            mixin.MixinName.Should().BeEquivalentTo(mixinName);
            Assert.IsTrue(mixin.MixinBody is RegularScope);
        }

        [TestMethod]
        public void ViewEngineVisitor_should_visit_mixin_declaration_with_template_body_properly()
        {
            // GIVEN
            var mixinName = "mixinName";

            var exp = $"mixin {mixinName} () <- Hello World ->";
            var parser = CreateTestParser(exp);

            // WHEN
            _testVisitor.Visit(parser.mixin_declaration());

            // THEN
            Assert.AreEqual(_testVisitor.Mixins.Count, 1);
            var mixin = _testVisitor.Mixins.First();
            mixin.MixinName.Should().BeEquivalentTo(mixinName);
            Assert.IsTrue(mixin.MixinBody is TemplateScope);
        }

        [TestMethod]
        public void ViewEngineVisitor_should_fail_when_mixin_declaration_has_no_name()
        {
            // GIVEN
            var exp = "mixin () <- Hello World ->";
            var parser = CreateTestParser(exp);

            // WHEN
            Action action = () => { _testVisitor.Visit(parser.mixin_declaration()); };

            // THEN
            Assert.ThrowsException<RenderException>(action);
        }
        #endregion

        #region Code Line Visit Tests
        [TestMethod]
        public void ViewEngineVisitor_should_visit_code_line_expression_properly()
        {
            // GIVEN
            var testCodeLine = "for(int i=0; i<5; i++) i++";

            var exp = $"--{testCodeLine}";
            var parser = CreateTestParser(exp);

            // WHEN
            _testVisitor.Visit(parser.regular_statement());

            // THEN
            Assert.AreEqual(_testVisitor.Result.Count, 1);
            var codeExp = (CodeLineExpression)_testVisitor.Result.First();
            codeExp.CodeLine.Should().BeEquivalentTo(testCodeLine);
        }
        #endregion
    }
}
