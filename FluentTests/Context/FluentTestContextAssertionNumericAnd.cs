using FluentAssertions.Numeric;

namespace FluentTests.Context;

public class
    FluentTestContextAssertionNumericAnd<TIn, TNumeric> : FluentTestContextAssertion<TIn,
        AndConstraint<NumericAssertions<TNumeric>>>
    where TNumeric : struct, IComparable<TNumeric>
{
    public FluentTestContextAssertionNumericAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<NumericAssertions<TNumeric>>> testSteps,
        string stepName, string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionNumeric<TIn, TNumeric> And() =>
        new(this, AddStep(and => and.And), "And");
}