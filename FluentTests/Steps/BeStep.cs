namespace FluentTests.Steps;

public class BeStep<T> : FluentTestStep<T, T> where T : class
{
    public BeStep(FluentTestStep previousStep, T expectedValue)
    {
        PreviousStep = previousStep;
        previousStep.NextStep = this;
        StepDescription = expectedValue.ToString();
        TestStepFunction = value =>
        {
            value.Should().Be(expectedValue);
            
            return value;
        };
    }
}

public class BeEquivalentToStep<T> : BeStep<T> where T : class
{
    public BeEquivalentToStep(FluentTestStep previousStep, T expectedValue) : base(previousStep, expectedValue)
    {
        TestStepFunction = value =>
        {
            value.Should().BeEquivalentTo(expectedValue);
            
            return value;
        };
    }
}