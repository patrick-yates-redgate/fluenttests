using FluentAssertions.Specialized;
using FluentTests.Context;

namespace FluentTests;

public class FluentTestContext<TIn, TOut> : FluentTestContextBase
{
    protected FluentTestContext(FluentTest? fluentTest, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(fluentTest, stepName, stepContentsDescription)
    {
        InvokeTestSteps = testSteps;
    }

    public IEnumerable<TIn> InputValues { get; set; }

    public Action<ActionAssertions>? ActionAssertion { get; set; } 
    
    public Func<TIn, TOut>? InvokeTestSteps { get; set; }

    public Func<TIn, TOut> AddStep(Func<TOut, TOut> func) => value => func(InvokeTestSteps!(value));
    public Func<TIn, TNewOut> AddStep<TNewOut>(Func<TOut, TNewOut> func) => value => func(InvokeTestSteps!(value));

    public Func<TIn, TOut> AddStep(Action<TOut> action) => value =>
    {
        var newValue = InvokeTestSteps!(value);
        action(newValue);
        return newValue;
    };

    public FluentTestContext<TIn, TOut> WithActionAssertion(Action<ActionAssertions> act)
    {
        ActionAssertion = act;
        return this;
    }

    public override void InvokeTest()
    {
        var act = () =>
        {
            InvokeTestSteps!(default!);
        };

        if (ActionAssertion == null)
        {
            act();
        }
        else
        {
            ActionAssertion.Invoke(act.Should());
        }
    }
}

public abstract class FluentTestContext<TContext, TIn, TOut> : FluentTestContext<TIn, TOut>,
    IFluentTestContext<TContext>
    where TContext : FluentTestContext<TContext, TIn, TOut>
{
    protected FluentTestContext(FluentTest? fluentTest, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(fluentTest, testSteps, stepName, stepContentsDescription)
    {
    }

    public abstract TContext GetThis();
}