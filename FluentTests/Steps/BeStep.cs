using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace FluentTests.Steps;

public class BeStep<T> : FluentTestFromShouldStep<T> where T : class
{
    private string? StepMethod { get; }

    private BeStep(FluentTestStep? previousStep, string? stepDescription = null) : base(previousStep, stepDescription)
    {
        TestStepFunction = _ =>
            throw new InvalidOperationException("Invalid usage, Expected public constructor to implement this test step");
    }

    private BeStep(FluentTestStep? previousStep, T expectedValue, string? stepDescription = null) : base(previousStep, expectedValue, stepDescription)
    {
        TestStepFunction = _ =>
            throw new InvalidOperationException("Invalid usage, Expected public constructor to implement this test step");
    }

    public BeStep(FluentTestStep? previousStep, string? stepMethod,
        Func<ObjectAssertions, AndConstraint<ObjectAssertions>> fluentAssertion, string? stepDescription = null) : this(previousStep, stepDescription)
    {
        StepMethod = stepMethod;
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }

    public BeStep(FluentTestStep? previousStep, T expectedValue, string? stepMethod,
        Func<ObjectAssertions, AndConstraint<ObjectAssertions>> fluentAssertion, string? stepDescription = null) : this(previousStep, expectedValue, stepDescription)
    {
        StepMethod = stepMethod;
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }

    public override string? StepPrefix() => StepMethod;
}

public class BeStepInt : FluentTestFromShouldStep<NumberWrapperInt>
{
    private string? StepMethod { get; }

    public BeStepInt(FluentTestStep? previousStep, string? stepMethod,
        Func<NumericAssertions<int>, AndConstraint<NumericAssertions<int>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, stepDescription)
    {
        StepMethod = stepMethod;
        TestStepFunction = value =>
        {
            fluentAssertion(value.Value.Should());

            return value;
        };
    }

    public BeStepInt(FluentTestStep? previousStep, int expectedValue, string? stepMethod,
        Func<NumericAssertions<int>, AndConstraint<NumericAssertions<int>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, new NumberWrapperInt(expectedValue), stepDescription)
    {
        StepMethod = stepMethod;
        TestStepFunction = value =>
        {
            fluentAssertion(value.Value.Should());

            return value;
        };
    }

    public override string? StepPrefix() => StepMethod;
}

public class BeStepFloat : FluentTestFromShouldStep<NumberWrapperFloat>
{
    private string? StepMethod { get; }

    public BeStepFloat(FluentTestStep? previousStep, string? stepMethod,
        Func<NumericAssertions<float>, AndConstraint<NumericAssertions<float>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, stepDescription)
    {
        StepMethod = stepMethod;
        TestStepFunction = value =>
        {
            fluentAssertion(value.Value.Should());

            return value;
        };
    }

    public BeStepFloat(FluentTestStep? previousStep, float expectedValue, string? stepMethod,
        Func<NumericAssertions<float>, AndConstraint<NumericAssertions<float>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, new NumberWrapperFloat(expectedValue), stepDescription)
    {
        StepMethod = stepMethod;
        TestStepFunction = value =>
        {
            fluentAssertion(value.Value.Should());

            return value;
        };
    }

    public override string? StepPrefix() => StepMethod;
}