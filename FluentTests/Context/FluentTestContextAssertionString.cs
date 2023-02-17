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
        new(this, AddStep(should => should.Be(value)), "Be", value);
    
    public FluentTestContextAssertionString<TIn> Throw<TException>(string because = "")
        where TException : Exception =>
        (new FluentTestContextAssertionString<TIn>(this, AddStep(should => should), "Throw",
            typeof(TException).Name).WithActionAssertion(should => { should.Throw<TException>(because); }) as FluentTestContextAssertionString<TIn>)!;
    
    public FluentTestContextAssertionString<TIn> NotThrow<TException>(string because = "")
        where TException : Exception =>
        (new FluentTestContextAssertionString<TIn>(this, AddStep(should => should), "NotThrow",
            typeof(TException).Name).WithActionAssertion(should => { should.NotThrow<TException>(because); }) as FluentTestContextAssertionString<TIn>)!;

}