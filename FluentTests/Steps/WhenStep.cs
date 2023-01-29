namespace FluentTests.Steps;

public class WhenStep<T> : FluentTestBeforeShouldStep<T> where T : class
{
    public WhenStep(FluentTestStep? previousStep, Action<T> action) : base(previousStep, action)
    {
    }

    public WhenStep(FluentTestStep? previousStep, Func<T, T> manipulationFunction) : base(previousStep,
        manipulationFunction)
    {
    }
}