namespace FluentTests.Context;

public class FluentTestContextArrange<TIn, TOut> :
    FluentTestContext<FluentTestContextArrange<TIn, TOut>, TIn, TOut>,
    IFluentTestContextArrange<FluentTestContextArrange<TIn, TOut>>
{
    public FluentTestContextArrange(Func<TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(null, _ => testSteps(),
        stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextArrange(FluentTestContextBase? context, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context?.FluentTest, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public override FluentTestContextArrange<TIn, TOut> GetThis() => this;

    public FluentTestContextAction<TIn, TOut> When(Action<TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TOut> When(Func<TOut, TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TOut> When(string stepContentsDescription,
        Action<TOut> stepFunction) =>
        new(this, AddStep(stepFunction), "When", stepContentsDescription);

    public FluentTestContextAction<TIn, TNewOut> When<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) =>
        new(this, AddStep(stepFunction), "When", stepContentsDescription);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(
        Func<TOut, TNewOut> stepFunction) => Then(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);
}