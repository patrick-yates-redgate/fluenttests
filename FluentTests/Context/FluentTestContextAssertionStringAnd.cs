using FluentAssertions.Primitives;

namespace FluentTests.Context;

public class FluentTestContextAssertionStringAnd<TIn> : FluentTestContextAssertion<TIn, AndConstraint<StringAssertions>>
{
    public FluentTestContextAssertionStringAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<StringAssertions>> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionString<TIn> And() =>
        new(this, AddStep(and => and.And), "And");
}