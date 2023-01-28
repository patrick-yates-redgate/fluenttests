namespace FluentTests.Steps;

public class ShouldStep<T> : FluentTestStep<T> where T : class
{
    public ShouldStep(FluentTestStep previousStep)
    {
        PreviousStep = previousStep;
        previousStep.NextStep = this;
        TestStepFunction = value => value;
    }

    public BeStep<T> Be(T value) => new (this, value);
    public BeEquivalentToStep<T> BeEquivalentTo(T value) => new (this, value);
}