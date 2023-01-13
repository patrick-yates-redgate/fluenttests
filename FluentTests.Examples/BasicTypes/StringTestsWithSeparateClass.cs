using System.Collections;
using static FluentTests.FluentTests;

namespace FluentTests.Examples.BasicTypes;

[TestFixture]
public class StringTestsWithSeparateClassRunner : FluentTestRunner<StringTestsWithSeparateClass>
{
}

public class StringTestsWithSeparateClass : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return Given("A").Should().Be("A");
        yield return Given("A").When(Lowercase).Should().Be("a");
        yield return Given("A").When(DoubleString).Should().Be("AA");
        yield return Given("ABC").When(Reverse).Should().Be("CBA");
    }

    public string DoubleString(string value) => value + value;

    public string Lowercase(string value) => value.ToLower();
    
    public string Reverse(string value)
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