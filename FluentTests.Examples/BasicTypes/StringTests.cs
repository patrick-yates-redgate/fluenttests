using System.Collections;
using static FluentTests.FluentTests;

namespace FluentTests.Examples.BasicTypes;

public class StringTests : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return Given("A").Should().Be("A");
        yield return Given("A").When(Lowercase).Should().Be("a");
        yield return Given("A").When(DoubleString).Should().Be("AA");
    }

    public string DoubleString(string value) => value + value;

    public string Lowercase(string value) => value.ToLower();
}

public class StringTestRunner : FluentTestBase
{
    [TestCaseSource(typeof(StringTests))]
    public void Test()
    {
        var properties = TestContext.CurrentContext.Test.Properties;

        FluentTestAction action = properties.Get("FluentTestAction") as FluentTestAction;

        action.ExecuteTest();
    }
}