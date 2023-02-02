using FluentTests.Steps;
using static FluentTests.FluentTestStaticMethods;

namespace FluentTests.Examples.Coding_Kata;

public static class GreetingKata
{
    public static BoolWrapper IsUppercase(string name) => new (name.All(Char.IsUpper));

    public static string Greeting(string? name)
    {
        if (name == null) return "Hello, my friend";

        return IsUppercase(name).Value ? $"HELLO {name}!" : $"Hello, {name}.";
    }

    public static string Greeting(IEnumerable<string> names)
    {
        return "Hello Jill and Jane";
    }
}

[TestFixture]
public class GreetingKataTests
{
    [FluentTestCasesBase]
    public void RunTest(FluentTestStep testStep) => testStep.InvokeTest();

    [FluentTestCases]
    public static IEnumerable<FluentTestStep> GreetingTests()
    {
        yield return Given((null as string)!).When(GreetingKata.Greeting).Should().Be("Hello, my friend");
        yield return Given("Bob").When(GreetingKata.Greeting).Should().Be("Hello, Bob.");
        yield return Given("John").When(GreetingKata.Greeting).Should().Be("Hello, John.");
        yield return Given("TOM").When(GreetingKata.Greeting).Should().Be("HELLO TOM!");
        yield return Given("JERRY").When(GreetingKata.Greeting).Should().Be("HELLO JERRY!");

        yield return Given(new string[2] { "Jill", "Jane" }).Then(GreetingKata.Greeting).Should()
            .Be("Hello Jill and Jane");
    }
    
    [FluentTestCases]
    public static IEnumerable<FluentTestStep> IsUppercaseTests()
    {
        yield return Given("Bob").Then(GreetingKata.IsUppercase).Should().BeEquivalentTo(new BoolWrapper(false));
        yield return Given("JERRY").Then(GreetingKata.IsUppercase).Should().BeEquivalentTo(new BoolWrapper(true));
        
        
    }
}