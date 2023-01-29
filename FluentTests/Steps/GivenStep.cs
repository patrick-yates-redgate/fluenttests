namespace FluentTests.Steps;

public class GivenStep<T> : FluentTestBeforeShouldStep<T> where T : class
{
    public GivenStep(Func<T> getFunction, string? stepDescription = null) : base(null, getFunction, stepDescription)
    {
    }
}