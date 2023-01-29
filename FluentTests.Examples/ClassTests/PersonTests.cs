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
        yield return Given(PersonA).Then(Name).Should().Be("A");
        yield return Given(PersonB).Then(Name).Should().Be("B");
        yield return Given(PersonA).Then("Name",x => x.Name).Should().Be("A");
        yield return Given(PersonA).When(SetAgeTo3).Then(Age).Should().Be(3);
        yield return Given(PersonAged30).When(HaveBirthday).Then(Age).Should().Be(31);
    }

    private static void HaveBirthday(Person person) => person.HaveBirthday();

    public static string Name(Person person) => person.Name;
    public static int Age(Person person) => person.Age;

    public static void SetAgeTo3(Person person) => person.Age = 3;

    public static Person PersonA() => new ("A");

    public static Person PersonB() => new ("B");
    public static Person PersonAged30() => new ("Unnamed Person") { Age = 30};
}