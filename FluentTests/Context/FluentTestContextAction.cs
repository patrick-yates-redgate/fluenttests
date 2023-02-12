namespace FluentTests.Context;

public class FluentTestContextAction<TIn, TOut> :
    FluentTestContext<FluentTestContextAction<TIn, TOut>, TIn, TOut>,
    IFluentTestContextAction<FluentTestContextAction<TIn, TOut>>
{
    public FluentTestContextAction(FluentTestContextBase? context, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context?.FluentTest, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public override FluentTestContextAction<TIn, TOut> GetThis() => this;
    
    public FluentTestContextAction<TIn, TOut> Then(string stepContentsDescription,
        Action<TOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);
    
    public FluentTestContextAction<TIn, TOut> Then(string stepContentsDescription,
        Func<TOut, TOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(
        Func<TOut, TNewOut> stepFunction) => Then(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);
    
    public FluentTestContextArrange<TIn, TOut> And => new(this, InvokeTestSteps!, "And");
}