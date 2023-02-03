namespace FluentTests.Steps;

public class AndStep<T> : FluentTestBeforeShouldStep<T>
{
    public AndStep(FluentTestStep? previousStep, Action<T> action) : base(previousStep, action)
    {
    }

    public AndStep(FluentTestStep? previousStep, Func<T, T> manipulationFunction) : base(previousStep,
        manipulationFunction)
    {
    }

    public AndStep<T> And(Action<T> action) => new (this, action);
}