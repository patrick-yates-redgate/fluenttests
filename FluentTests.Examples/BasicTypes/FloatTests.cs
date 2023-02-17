using FluentAssertions;
using FluentTests.Context;
using static FluentTests.FluentTests;

namespace FluentTests.Examples.BasicTypes;

public static class FloatMathLibraryUnderTest
{
    public static float MultiplyBy2(float value) => value * 2f;
    public static float MultiplyBySelf(float value) => value * value;
    public static float DivideBy0(float value) => value / 0f;
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
        yield return Given(5.1f).Should().NotBeInRange(2f, 5f);
        yield return Given(5f).Should().BeOneOf(2f, 5f);
        yield return Given(1f).When(FloatMathLibraryUnderTest.MultiplyBy2).Should().Be(2f);
        yield return Given(-34f).When(FloatMathLibraryUnderTest.MultiplyBySelf).Should().BeGreaterOrEqualTo(0f);
        yield return Given(-34f).When(FloatMathLibraryUnderTest.MultiplyBySelf).Should().BePositive();
        
        yield return Given(1f).When(FloatMathLibraryUnderTest.DivideBy0).Should().NotThrow<DivideByZeroException>();
        yield return Given(1f).When(FloatMathLibraryUnderTest.DivideBy0).Should().NotThrow<NullReferenceException>();
        
        yield return Given(1.1234f).Should().BeApproximately(1.123f, 0.001f);
        yield return Given(1.1234f).Should().NotBeApproximately(1.123f, 0.0001f);
        //123f.Should().BeApproximately()
    }

    public static IEnumerable<float> TestValues => new[] { -1f, 2f, 1000f, 3242f };
}