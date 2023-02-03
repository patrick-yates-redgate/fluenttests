namespace FluentTests.Steps;

public class GivenStep<T> : FluentTestBeforeShouldStep<T>
{
    public GivenStep(Func<T> getFunction, string? stepDescription = null) : base(null, getFunction, stepDescription)
    {
    }
}