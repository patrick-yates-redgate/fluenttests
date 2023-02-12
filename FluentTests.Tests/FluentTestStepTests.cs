using FluentAssertions;

namespace FluentTests.Tests;

[TestFixture]
public class FluentTestStepTests
{
    private Fixture Fixture { get; } = new();

    public string AddPrefix(string value) => "Prefix" + value;

    public string InitialState() => "InitialState";

    [Test]
    public void TestThatWhenWeExecuteAGivenTest_WithJustOneMethod_WeExecuteThatMethod()
    {
        var method = new Mock<Action<string>>();

        Given("InitialState").When(method.Object).InvokeTest();
        
        method.Verify(action => action(Moq.It.IsAny<string>()), Times.Once());
    }

    /*
    [Test]
    public void TestThatWeHaveTheRightTestName_WhenUsingAnActionForInitialValue()
    {
        var test = Given(InitialState);
        test.NameParts.Count.Should().Be(1);
        test.NameParts[0].Should().Be("Given(InitialState)");
    }

    [Test]
    public void TestThatWeHaveTheRightTestName_WhenUsingAStringLiteralForInitialValue()
    {
        var test = Given("InitialState");
        test.NameParts.Count.Should().Be(1);
        test.NameParts[0].Should().Be("Given(InitialState)");
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
    
    private string Combine(Tuple<string, string> values) => values.Item1 + values.Item2;
    
    [Test]
    public void TestThatWeHaveTheRightTestName_WhenUsingThen()
    {
        
        var test = Given(new Tuple<string, string>("A", "B")).Then(Combine);
        test.NameParts.Count.Should().Be(2);
        test.NameParts[0].Should().Be("Given((A, B))");
        test.NameParts[1].Should().Be("Then(Combine)");
    }
    
    */
    
    [Test]
    public void Test_FluentTestStep_EmptyString()
    {
        var test = new FluentTestStep { StepName = "Given", StepContentsDescription = string.Empty };

        test.TestStepName.Should().Be("Given(Empty)");
    }
    
    [Test]
    public void Test_Given_EmptyString()
    {
        var test = Given(string.Empty);

        test.NameParts.Should().BeEquivalentTo("Given(Empty)");
    }

    [Test]
    public void Test_Given_When_Should_On_EmptyString()
    {
        var test = Given(string.Empty).When(AppendX).Should().Be("X");

        test.NameParts.Should().BeEquivalentTo("Given(Empty)", "When(AppendX)", "Should", "Be(X)");
    }

    [Test]
    public void Test_Given_When_Should_On_SimpleString()
    {
        var test = Given("Y").When(AppendX).Should().Be("YX");

        test.NameParts.Should().BeEquivalentTo("Given(Y)", "When(AppendX)", "Should", "Be(YX)");
    }

    [Test]
    public void Test_Given_Should_On_SimpleInt()
    {
        var test = Given(123).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionNumeric<int, int>>();
        test.NameParts.Should().BeEquivalentTo("Given(123)", "Should");
    }

    [Test]
    public void Test_Given_NullString_Should()
    {
        var test = Given((null as string)!).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionString<string>>();
        test.NameParts.Should().BeEquivalentTo("Given(null)", "Should");
    }

    [Test]
    public void Test_Given_When_On_ChangeFromStringToInt_ShouldHaveRightTypeWithInAndOut()
    {
        var test = Given("123").When("Length", x => x.Length).Should().Be(3);
            
        test.GetType().Should().Be<FluentTestContextAssertionNumericAnd<string, int>>();
        test.NameParts.Should().BeEquivalentTo("Given(123)", "When(Length)", "Should", "Be(3)");
    }

    [Test]
    public void Test_BeAndBePositive_On_ChangeFromStringToInt_ShouldHaveRightTypeWithInAndOut()
    {
        var test = Given("123").When("Length", x => x.Length).Should().Be(3).And().BePositive();
            
        test.GetType().Should().Be<FluentTestContextAssertionNumericAnd<string, int>>();
        test.NameParts.Should().BeEquivalentTo("Given(123)", "When(Length)", "Should", "Be(3)", "And", "BePositive");
    }

    private string AppendX(string input) => input + "X";

    /*
    [FluentTestCasesBase]
    public void RunTest(FluentTest testStep) => testStep.InvokeTest();
    
    [FluentTestCases]
    public static IEnumerable<FluentTestContext> MyTests()
    {
        yield return Given(NewFluentTestStep).When("Set Name", step =>
        {
            step.StepName = "StepName";
        }).Then("StepName", step => step.StepName)!.Should().Be("StepName");
    }

    private static FluentTestStep NewFluentTestStep() => new ();
    */
}