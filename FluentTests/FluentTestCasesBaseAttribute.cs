using System.Reflection;
using FluentTests.Steps;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace FluentTests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class FluentTestCasesBaseAttribute : NUnitAttribute, ITestBuilder
{
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test? suite)
    {
        var testCaseMethods = method.MethodInfo.DeclaringType.Methods()
            .Where(x => x.GetCustomAttribute<FluentTestCasesAttribute>(true) != null);

        foreach (var testCaseMethod in testCaseMethods)
        {
            var fluentTestCases = (IEnumerable<FluentTest>) testCaseMethod?.Invoke(null, null);
            foreach (var fluentTestCase in fluentTestCases)
            {
                var testCaseParameters = new TestCaseParameters(new object?[]
                {
                    fluentTestCase
                });
                testCaseParameters.TestName = string.Join("_", fluentTestCase.NameParts);
            
                yield return new NUnitTestCaseBuilder().BuildTestMethod(method, suite, testCaseParameters);
            }   
        }
    }
}