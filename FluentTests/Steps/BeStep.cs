using System.Formats.Asn1;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace FluentTests.Steps;

public class BeStep<T> : FluentTestFromShouldStep<T>
{
    private string? StepMethod { get; set; }

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

    private BeStep(FluentTestStep? previousStep, Func<T> expectedValueFunc, string? stepDescription = null) : base(previousStep, expectedValueFunc, stepDescription)
    {
        TestStepFunction = _ =>
            throw new InvalidOperationException("Invalid usage, Expected public constructor to implement this test step");
    }
    
    protected BeStep(FluentTestStep? previousStep, string? stepMethod, string? stepDescription = null) : this(previousStep, stepDescription)
    {
        StepMethod = stepMethod;
    }

    protected BeStep(FluentTestStep? previousStep, T expectedValue, string? stepMethod, string? stepDescription = null) : this(previousStep, expectedValue, stepDescription)
    {
        StepMethod = stepMethod;
    }

    protected BeStep(FluentTestStep? previousStep, Func<T> expectedValueFunc, string? stepMethod, string? stepDescription = null) : this(previousStep, expectedValueFunc, stepDescription)
    {
        StepMethod = stepMethod;
    }

    public override string? StepPrefix() => StepMethod;
}

public class BeStepObject : BeStep<object>
{
    public BeStepObject(FluentTestStep? previousStep, string? stepMethod,
        Func<ObjectAssertions, AndConstraint<ObjectAssertions>> fluentAssertion, string? stepDescription = null) : base(previousStep, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }
    
    public BeStepObject(FluentTestStep? previousStep, object expectedValue, string? stepMethod,
        Func<ObjectAssertions, AndConstraint<ObjectAssertions>> fluentAssertion, string? stepDescription = null) : base(previousStep, expectedValue, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }

    public BeStepObject(FluentTestStep? previousStep, Func<object> expectedValueAction, string? stepMethod,
        Func<ObjectAssertions, AndConstraint<ObjectAssertions>> fluentAssertion, string? stepDescription = null) : base(previousStep, expectedValueAction, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }
}

public class BeStepInt : BeStep<int>
{
    public BeStepInt(FluentTestStep? previousStep, string? stepMethod,
        Func<NumericAssertions<int>, AndConstraint<NumericAssertions<int>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }

    public BeStepInt(FluentTestStep? previousStep, int expectedValue, string? stepMethod,
        Func<NumericAssertions<int>, AndConstraint<NumericAssertions<int>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, expectedValue, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }
}

public class BeStepFloat : BeStep<float>
{
    public BeStepFloat(FluentTestStep? previousStep, string? stepMethod,
        Func<NumericAssertions<float>, AndConstraint<NumericAssertions<float>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }

    public BeStepFloat(FluentTestStep? previousStep, float expectedValue, string? stepMethod,
        Func<NumericAssertions<float>, AndConstraint<NumericAssertions<float>>> fluentAssertion,
        string? stepDescription = null) : base(previousStep, expectedValue, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }
}

public class BeStepBool : BeStep<bool>
{
    public BeStepBool(FluentTestStep? previousStep, string? stepMethod,
        Func<BooleanAssertions, AndConstraint<BooleanAssertions>> fluentAssertion, string? stepDescription = null) : base(previousStep, stepMethod, stepDescription)
    {
        TestStepFunction = value =>
        {
            fluentAssertion(value.Should());

            return value;
        };
    }
}