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
    
    public static FluentTestContextArrange<TIn, TOut> Given(string stepContentsDescription, Func<TOut> valueFunc) =>
        new(null, _ => valueFunc(), "Given", stepContentsDescription);
    
    public static FluentTestContextArrange<TIn, TOut> Given(Func<TOut> valueFunc) => Given(valueFunc.Method.Name, valueFunc);

    public static FluentTestContextArrange<TIn, TOut> Given(TOut value) => Given(Describe(value), () => value);

    public override FluentTestContextArrange<TIn, TOut> GetThis() => this;

    public FluentTestContextAction<TIn, TOut> When(Action<TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TOut> When(Func<TOut, TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);
    
    public FluentTestContextAction<TIn, TOut> When<TParam1>(Func<TOut, TParam1, TOut> stepFunction, TParam1 paramValue1) =>
        When(stepFunction.Method.Name, stepFunction, paramValue1);
    
    public FluentTestContextAction<TIn, TOut> When<TParam1, TParam2>(Func<TOut, TParam1, TParam2, TOut> stepFunction, TParam1 paramValue1, TParam2 paramValue2) =>
        When(stepFunction.Method.Name, stepFunction, paramValue1, paramValue2);

    public FluentTestContextAction<TIn, TOut> When(string stepContentsDescription,
        Action<TOut> stepFunction) =>
        new(this, AddStep(stepFunction), "When", stepContentsDescription);

    public FluentTestContextAction<TIn, TNewOut> When<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) =>
        new(this, AddStep(stepFunction), "When", stepContentsDescription);
    
    public FluentTestContextAction<TIn, TOut> When<TParam1>(string stepContentsDescription, Func<TOut, TParam1, TOut> stepFunction, TParam1 paramValue1) =>
        When(stepContentsDescription + "(" + Describe(paramValue1) + ")", inValue => stepFunction(inValue, paramValue1));
    
    public FluentTestContextAction<TIn, TOut> When<TParam1, TParam2>(string stepContentsDescription, Func<TOut, TParam1, TParam2, TOut> stepFunction, TParam1 paramValue1, TParam2 paramValue2) =>
        When(stepContentsDescription + "(" + Describe(paramValue1) + "," + Describe(paramValue2) + ")", inValue => stepFunction(inValue, paramValue1, paramValue2));

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(
        Func<TOut, TNewOut> stepFunction) => Then(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);
}