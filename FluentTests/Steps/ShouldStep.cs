namespace FluentTests.Steps;

public class ShouldStep<T> : FluentTestStep<T, T> where T : class
{
    public ShouldStep(FluentTestStep previousStep)
    {
        PreviousStep = previousStep;
        previousStep.NextStep = this;
        TestStepFunction = value => value;
    }

    public BeStep<T> Be(T value) => new (this, value);
    public BeStep<NumberWrapperInt> Be(int value) => new (this, new NumberWrapperInt(value));
    public BeStep<NumberWrapperFloat> Be(float value) => new (this, new NumberWrapperFloat(value));
    public BeEquivalentToStep<T> BeEquivalentTo(T value) => new (this, value);
}