using FluentAssertions.Primitives;

namespace FluentTests.Context;

public class
    FluentTestContextAssertionObjectAnd<TIn, TOut> : FluentTestContextAssertion<TIn, AndConstraint<ObjectAssertions>>
{
    public FluentTestContextAssertionObjectAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<ObjectAssertions>> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionObject<TIn, TOut> And =>
        new(this, AddStep(and => and.And), "And");
}