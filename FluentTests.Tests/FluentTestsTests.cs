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

    [Test]
    public void TestThatWhenGivenIsCalled_WithASingleMethod_WeGenerateTheRightTestName()
    {
        var state = FluentTests.Given<string>(TestMethodString);

        state.TestCaseName.Should().Be("GivenTestMethodString");
    }

    [Test]
    public void TestThatWhenGivenIsCalled_WithTwoMethods_WeGenerateTheRightTestNameWithAndBetween()
    {
        var state = FluentTests.Given<string>(TestMethodString, TestMethodString2);

        state.TestCaseName.Should().Be("GivenTestMethodStringAndTestMethodString2");
    }

    [Test]
    public void TestThatWhenWeExecuteAGivenTest_WithJustOneMethod_WeExecuteThatMethod()
    {
        var method = new Moq.Mock<Action<string>>();
        //method.Setup(action => action(Moq.It.IsAny<string>()));

        FluentTests.Given(method.Object).ExecuteTest?.Invoke(Fixture.Create<string>());
        
        method.Verify(action => action(Moq.It.IsAny<string>()), Times.Once());
    }
}