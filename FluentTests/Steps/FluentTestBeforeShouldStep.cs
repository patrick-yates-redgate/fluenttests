namespace FluentTests.Steps;

public class FluentTestBeforeShouldStep<T> : FluentTestStep<T, T> where T : class
{
    private FluentTestBeforeShouldStep(FluentTestStep? previousStep)
    {
        PreviousStep = previousStep;
        if (previousStep != null) previousStep.NextStep = this;
    }

    protected FluentTestBeforeShouldStep(FluentTestStep? previousStep, Action<T> action, string? stepDescription = null)
        : this(previousStep)
    {
        StepDescription = stepDescription ?? action.Method.Name;
        TestStepFunction = value =>
        {
            action(value);
            return value;
        };
    }

    protected FluentTestBeforeShouldStep(FluentTestStep? previousStep, Func<T> getFunction,
        string? stepDescription = null) : this(previousStep)
    {
        StepDescription = stepDescription ?? getFunction.Method.Name;
        TestStepFunction = _ => getFunction();
    }

    protected FluentTestBeforeShouldStep(FluentTestStep? previousStep, Func<T, T> manipulationFunction,
        string? stepDescription = null) : this(previousStep)
    {
        StepDescription = stepDescription ?? manipulationFunction.Method.Name;
        TestStepFunction = manipulationFunction;
    }

    public WhenStep<T> When(Action<T> action) => new(this, action);
    
    public WhenStep<T> When(Func<T, T> manipulationFunction) => new(this, manipulationFunction);

    public ThenStep<T, TOut>Then<TOut>(Func<T, TOut> transformFunc) where TOut : class =>
        new(this, transformFunc);

    public ThenStep<T, TOut> Then<TOut>(string stepDescription, Func<T, TOut> transformFunc) where TOut : class =>
        new(this, transformFunc, stepDescription);

    public ThenStep<T, T> Then(string stepDescription, Action<T> assertionAction) => new(this,
        value =>
        {
            assertionAction(value);
            return value;
        }, stepDescription);

    public ThenStep<T, NumberWrapperInt> Then(Func<T, int> transformFunc) =>
        new(this, value => new NumberWrapperInt(transformFunc(value)), transformFunc.Method.Name);

    public ThenStep<T, NumberWrapperFloat> Then(Func<T, float> transformFunc) =>
        new(this, value => new NumberWrapperFloat(transformFunc(value)), transformFunc.Method.Name);

    public ShouldStep<T> Should() => new(this);
}