using FluentTests.Context;
using static FluentTests.FluentTests;

namespace FluentTests.Examples.BasicTypes;

[TestFixture]
public class StringTests
{
    [FluentTestCasesBase]
    public void RunTest(FluentTestContextBase testStep) => testStep.InvokeTest();
    
    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> MyTests()
    {
        yield return Given("A").Should().Be("A");
        
        yield return Given("A").When(Lowercase).Should().Be("a");
        yield return Given("A").When(DoubleString).Should().Be("AA");
        
        yield return Given("ABC").When(Reverse).Should().Be("CBA");

        //yield return Given("Correct Input").When(BrokenMethod).Should().Be("Correct Output");
    }

    public static string DoubleString(string value) => value + value;

    public static string Lowercase(string value) => value.ToLower();

    public static string BrokenMethod(string value) => "This is broken";
    
    public static string Reverse(string value)
    {
        if (value.Length <= 1)
        {
            return value;
        }

        char[] chars = value.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}