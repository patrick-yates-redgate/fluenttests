using FluentAssertions;

namespace FluentTests.Tests;

[TestFixture]
public class FluentTestStepTests
{
    private Fixture Fixture { get; } = new();

    public string AddPrefix(string value) => "Prefix" + value;

    [Test]
    public void TestThatWhenWeExecuteAGivenTest_WithJustOneMethod_WeExecuteThatMethod()
    {
        var method = new Mock<Action<string>>();

        Given("InitialState").When(method.Object).InvokeTest();
        
        method.Verify(action => action(Moq.It.IsAny<string>()), Times.Once());
    }

    [Test]
    public void TestThat_ForAString_GivenAnInitialState_WeCanAddPrefix_AndUseFluentAssertionsOnTheOutput()
    {
        var state = Given("InitialState").When(AddPrefix).Should().Be("PrefixInitialState");

        state.NameParts.Count.Should().Be(4);
        state.NameParts[0].Should().Be("Given(InitialState)");
        state.NameParts[1].Should().Be("When(AddPrefix)");
        state.NameParts[2].Should().Be("Should");
        state.NameParts[3].Should().Be("Be(PrefixInitialState)");
    }
}