namespace FluentTests.Context;

public class FluentTestContextAssertion<TIn, TOut> :
    FluentTestContext<FluentTestContextAssertion<TIn, TOut>, TIn, TOut>,
    IFluentTestContextAssertion<FluentTestContextAssertion<TIn, TOut>>
{
    public override FluentTestContextAssertion<TIn, TOut> GetThis() => this;

    public FluentTestContextAssertion(FluentTestContextBase? context, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context?.FluentTest, testSteps, stepName,
        stepContentsDescription)
    {
    }
}