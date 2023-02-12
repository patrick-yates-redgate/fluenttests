using FluentAssertions.Primitives;

namespace FluentTests.Context;

public class FluentTestContextAssertionBooleanAnd<TIn> : FluentTestContextAssertion<TIn, AndConstraint<BooleanAssertions>>
{
    public FluentTestContextAssertionBooleanAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<BooleanAssertions>> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionBoolean<TIn> And() =>
        new(this, AddStep(and => and.And), "And");
}