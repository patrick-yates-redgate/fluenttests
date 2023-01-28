using FluentTests.Steps;
using static FluentTests.FluentTestStaticMethods;
namespace FluentTests.Examples.ClassTests;

[TestFixture]
public class PersonTests
{
    [FluentTestCasesBase]
    public void RunTest(FluentTestStep testStep) => testStep.InvokeTest();

    [FluentTestCases]
    public static IEnumerable<FluentTestStep> MyTests()
    {
        yield return Given(PersonA).Should().BeEquivalentTo(PersonA());
        //yield return Given(PersonA).Where(x => x.Name).Should().Be("A");
    }

    public static Person PersonA() => new Person();
}