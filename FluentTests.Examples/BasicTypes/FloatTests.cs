using FluentTests.Context;
using static FluentTests.FluentTests;

namespace FluentTests.Examples.BasicTypes;

public static class FloatMathLibraryUnderTest
{
    public static float MultiplyBy2(float value) => value * 2f;
    public static float MultiplyBySelf(float value) => value * value;
}

[TestFixture]
public class FloatTests
{
    [FluentTestCasesBase]
    public void RunTest(FluentTestContextBase testStep) => testStep.InvokeTest();

    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> MyTests()
    {
        yield return Given(1f).Should().Be(1f);
        yield return Given(1f).Should().NotBe(2f);
        yield return Given(1f).Should().BeGreaterThan(0);
        yield return Given(1f).Should().BeLessThan(2f);
        yield return Given(1f).Should().BeGreaterOrEqualTo(1f);
        yield return Given(2f).Should().BeGreaterOrEqualTo(1f);
        yield return Given(1f).Should().BeLessThanOrEqualTo(2f);
        yield return Given(2f).Should().BeLessThanOrEqualTo(2f);
        yield return Given(3f).Should().BeInRange(2f, 5f);
        yield return Given(5f).Should().BeOneOf(2f, 5f);
        
        yield return Given(1f).When(FloatMathLibraryUnderTest.MultiplyBy2).Should().Be(2f);
        //yield return Given(TestValues).When(MultiplyBySelf).Should().BeGreaterThanOrEqualTo(0);
    }

    public static IEnumerable<float> TestValues => new[] { -1f, 2f, 1000f, 3242f };
}