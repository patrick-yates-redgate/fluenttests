using FluentAssertions;
using FluentTests.Context;

namespace FluentTests.Tests;

[TestFixture]
public class FluentTestStepTests
{
    private Fixture Fixture { get; } = new();

    public string AddPrefix(string value) => "Prefix" + value;

    public string InitialState() => "InitialState";
    
    public int Add(int a, int b) => a + b;
    public float Add(float a, float b) => a + b;
    public int Add3(int a, int b, int c) => a + b + c;

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

    private string MyValueFunc() => "My value";
    private string MyValueFuncWithArg(string arg) => "My value " + arg;
    private string MyValueFuncWith2Args(string arg1, string arg2) => "My value " + arg1 + " + " + arg2;
    
    [Test]
    public void Test_Given_WithSimpleFunction()
    {
        var test = Given(MyValueFunc);

        test.NameParts.Should().BeEquivalentTo("Given(MyValueFunc)");
    }
    
    [Test]
    public void Test_Given_WithOneArgFunction()
    {
        var test = Given(MyValueFuncWithArg, "Arg1");

        test.NameParts.Should().BeEquivalentTo("Given(MyValueFuncWithArg(Arg1))");
    }
    
    [Test]
    public void Test_Given_WithTwoArgFunction()
    {
        var test = Given(MyValueFuncWith2Args, "Arg1", "Arg2");

        test.NameParts.Should().BeEquivalentTo("Given(MyValueFuncWith2Args(Arg1,Arg2))");
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
    public void Test_Given_Should_On_SimpleString()
    {
        var test = Given("123").Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionString<string>>();
        test.NameParts.Should().BeEquivalentTo("Given(123)", "Should");
    }

    [Test]
    public void Test_Given_Should_On_SimpleInt()
    {
        var test = Given(123).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionNumeric<int, int>>();
        test.NameParts.Should().BeEquivalentTo("Given(123)", "Should");
    }

    [Test]
    public void Test_Given_Should_On_SimpleDecimal()
    {
        var test = Given<decimal>(123).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionNumeric<decimal, decimal>>();
        test.NameParts.Should().BeEquivalentTo("Given(123)", "Should");
    }

    [Test]
    public void Test_Given_Should_On_SimpleFloat()
    {
        var test = Given(123f).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionNumeric<float, float>>();
        test.NameParts.Should().BeEquivalentTo("Given(123f)", "Should");
    }

    [Test]
    public void Test_Given_Should_On_SimpleDouble()
    {
        var test = Given(123d).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionNumeric<double, double>>();
        test.NameParts.Should().BeEquivalentTo("Given(123d)", "Should");
    }

    [Test]
    public void Test_Given_NullString_Should()
    {
        var test = Given((null as string)!).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionString<string>>();
        test.NameParts.Should().BeEquivalentTo("Given(null)", "Should");
    }

    [Test]
    public void Test_Given_IEnumerableString_Should()
    {
        var test = Given<IEnumerable<string>>(new []{ "A", "B", "C"}).Should();
            
        test.GetType().Should().Be<FluentTestContextAssertionObject<IEnumerable<string>, IEnumerable<string>>>();
        test.NameParts.Should().BeEquivalentTo("Given(A,B,C)", "Should");
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

    [Test]
    public void Test_WhenWithArguments()
    {
        var test = Given(1).When(Add, 2).Should().Be(3);
        
        test.NameParts.Should().BeEquivalentTo("Given(1)", "When(Add(2))", "Should", "Be(3)");
        test.InvokeTest();
    }

    [Test]
    public void Test_WhenWithArguments_AndNamedStep()
    {
        var test = Given(1).When("MyAdd", (int a, int b) => a + b, 2).Should().Be(3);
        
        test.NameParts.Should().BeEquivalentTo("Given(1)", "When(MyAdd(2))", "Should", "Be(3)");
        test.InvokeTest();
    }

    [Test]
    public void Test_WhenWith2Arguments()
    {
        var test = Given(1).When(Add3, 2, 3).Should().Be(6);
        
        test.NameParts.Should().BeEquivalentTo("Given(1)", "When(Add3(2,3))", "Should", "Be(6)");
        test.InvokeTest();
    }

    [Test]
    public void Test_WhenWith2Arguments_AndNamedStep()
    {
        var test = Given(1).When("MyAdd3", (int a, int b, int c) => a + b + c, 2, 3).Should().Be(6);
        
        test.NameParts.Should().BeEquivalentTo("Given(1)", "When(MyAdd3(2,3))", "Should", "Be(6)");
        test.InvokeTest();
    }

    [Test]
    public void Test_WhenWithFloatArgument()
    {
        var test = Given(1f).When(Add, 2f).Should().Be(3);
        
        test.NameParts.Should().BeEquivalentTo("Given(1f)", "When(Add(2f))", "Should", "Be(3)");
        test.InvokeTest();
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