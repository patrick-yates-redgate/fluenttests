namespace FluentTests.Steps;

public class WhenStep<T> : FluentTestBeforeShouldStep<T>
{
    public WhenStep(FluentTestStep? previousStep, Action<T> action) : base(previousStep, action)
    {
    }

    public WhenStep(FluentTestStep? previousStep, Func<T, T> manipulationFunction) : base(previousStep,
        manipulationFunction)
    {
    }

    public AndStep<T> And(Action<T> action) => new (this, action);

    public AndStep<T> And(Func<T, T> manipulationFunction) => new (this, manipulationFunction);
}