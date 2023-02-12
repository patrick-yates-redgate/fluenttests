using FluentAssertions;
using FluentTests.Steps;
using static FluentTests.FluentTests;

namespace FluentTests.Examples.BasicTypes;

public static class IntMathLibraryUnderTest
{
    public static int MultiplyBy2(int value) => value * 2;
    
    // ReSharper disable once IntDivisionByZero
    public static int DivideBy0(int value) => value / 0;
}

[TestFixture]
public class IntTests
{
    [FluentTestCasesBase]
    public void RunTest(FluentTestContextBase testStep) => testStep.InvokeTest();

    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> MyTests()
    {
        yield return Given(1).Should().Be(1);
        yield return Given(1).Should().NotBe(2);
        yield return Given(1).Should().BeGreaterThan(0);
        yield return Given(1).Should().BeLessThan(2);
        yield return Given(1).Should().BeGreaterOrEqualTo(1);
        yield return Given(2).Should().BeGreaterOrEqualTo(1);
        yield return Given(1).Should().BeLessThanOrEqualTo(2);
        yield return Given(2).Should().BeLessThanOrEqualTo(2);
        yield return Given(1).Should().BePositive();
        yield return Given(-1).Should().BeNegative();
        
        yield return Given(1).When(IntMathLibraryUnderTest.MultiplyBy2).Should().Be(2);
        
        //yield return Given(1).When(IntMathLibraryUnderTest.DivideBy0).Should().Throw(new DivideByZeroException());
    }

    public static IEnumerable<int> TestValues => new[] { -1, 2, 1000, 3242 };
}