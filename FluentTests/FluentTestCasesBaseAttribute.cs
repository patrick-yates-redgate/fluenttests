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
        var tests = new List<TestMethod>();
        
        try
        {
            var testCaseMethods = method.MethodInfo.DeclaringType.Methods()
                .Where(x => x.GetCustomAttribute<FluentTestCasesAttribute>(true) != null);

            foreach (var testCaseMethod in testCaseMethods)
            {
                var fluentTestCases = (IEnumerable<FluentTestContextBase>)testCaseMethod?.Invoke(null, null);
                foreach (var fluentTestCase in fluentTestCases)
                {
                    var testCaseParameters = new TestCaseParameters(new object?[]
                    {
                        fluentTestCase
                    });
                    testCaseParameters.TestName = string.Join("_", fluentTestCase.NameParts);

                    tests.Add(new NUnitTestCaseBuilder().BuildTestMethod(method, suite, testCaseParameters));
                }
            }
        }
        catch (Exception ex)
        {
            tests.Add(new NUnitTestCaseBuilder().BuildTestMethod(method, suite, new TestCaseParameters(new object?[]
            {
                new FluentTestContextArrange<object, object>(() => { throw new Exception("Unable to build tests", ex); }, "Setup Error")
            })));
        }

        return tests;
    }
}