using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FluentTests;

[TestFixture]
public class FluentTestRunner<T> where T : IEnumerable, new()
{
    private static T? GetEnumeratorForT()
    {
        return new();
    }
    
    [TestCaseSource(nameof(GetEnumeratorForT))]
    public void InternalRunTests()
    {
        var properties = TestContext.CurrentContext.Test.Properties;

        FluentTestAction action = properties.Get("FluentTestAction") as FluentTestAction;

        action.ExecuteTest();
    }
}