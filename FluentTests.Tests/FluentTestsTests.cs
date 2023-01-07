using NUnit.Framework;

namespace FluentTests.Tests;

[TestFixture]
public class FluentTestsTests
{
    private Fixture Fixture { get; } = new();

    public void TestMethodString(string value)
    {
    }
    
    public void TestMethodString2(string value)
    {
    }

    public string AddPrefix(string value) => "Prefix" + value;

    [Test]
    public void TestThatWhenGivenIsCalled_WithASingleMethod_WeGenerateTheRightTestName()
    {
        var state = FluentTests.Given("InitialState", TestMethodString);

        state.TestCaseName.Should().Be("GivenInitialStateAndTestMethodString");
    }

    [Test]
    public void TestThatWhenGivenIsCalled_WithTwoMethods_WeGenerateTheRightTestNameWithAndBetween()
    {
        var state = FluentTests.Given("InitialState", TestMethodString, TestMethodString2);

        state.TestCaseName.Should().Be("GivenInitialStateAndTestMethodStringAndTestMethodString2");
    }

    [Test]
    public void TestThatWhenWeExecuteAGivenTest_WithJustOneMethod_WeExecuteThatMethod()
    {
        var method = new Mock<Action<string>>();

        FluentTests.Given("InitialState", method.Object).ExecuteTest();
        
        method.Verify(action => action(Moq.It.IsAny<string>()), Times.Once());
    }

    [Test]
    public void TestThat_ForAString_GivenAnInitialState_WeCanAddPrefix_AndUseFluentAssertionsOnTheOutput()
    {
        var state = FluentTests.Given("InitialState").When(AddPrefix).Should().Be("PrefixInitialState");

        state.TestCaseName.Should().Be("GivenInitialState_WhenAddPrefix_ShouldBePrefixInitialState");
    }
}