using FluentAssertions.Primitives;

namespace FluentTests.Context;

public class FluentTestContextAssertionString<TIn> : FluentTestContextAssertion<TIn, StringAssertions>
{
    public FluentTestContextAssertionString(FluentTestContextBase? context, Func<TIn, StringAssertions> testSteps,
        string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionStringAnd<TIn> Be(string value) =>
        new(this, AddStep(should => should.Be(value)), "Be", value.ToString());
}