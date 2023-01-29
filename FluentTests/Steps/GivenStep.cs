namespace FluentTests.Steps;

public class GivenStep<T> : FluentTestStep<T, T> where T : class
{
    public GivenStep(string description, Func<T> getFunction)
    {
        StepDescription = description;
        TestStepFunction = _ => getFunction();
    }

    public WhenStep<T> When(Action<T> action) => new(this, action);
    
    public WhenStep<T> When(Func<T, T> manipulationFunction) => new(this, manipulationFunction);

    public ThenStep<T, TOut> Then<TOut>(Func<T, TOut> transformFunc) where TOut : class =>
        new(this, transformFunc);
    
    public ThenStep<T, TOut> Then<TOut>(string stepDescription, Func<T, TOut> transformFunc) where TOut : class =>
        new(this, transformFunc, stepDescription);

    public ShouldStep<T> Should() => new(this);
}