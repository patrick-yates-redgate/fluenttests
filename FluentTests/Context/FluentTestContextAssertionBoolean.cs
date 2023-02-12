using FluentAssertions.Primitives;

namespace FluentTests.Context;

public class FluentTestContextAssertionBoolean<TIn> : FluentTestContextAssertion<TIn, BooleanAssertions>
{
    public FluentTestContextAssertionBoolean(FluentTestContextBase? context, Func<TIn, BooleanAssertions> testSteps,
        string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionBooleanAnd<TIn> Be(bool value) =>
        new(this, AddStep(should => should.Be(value)), "Be", value.ToString());
    public FluentTestContextAssertionBooleanAnd<TIn> BeTrue() =>
        new(this, AddStep(should => should.BeTrue()), "BeTrue");
    public FluentTestContextAssertionBooleanAnd<TIn> BeFalse() =>
        new(this, AddStep(should => should.BeFalse()), "BeFalse");
}