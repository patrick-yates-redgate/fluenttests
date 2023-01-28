namespace FluentTests.Steps;

public class WhenStep<T> : FluentTestStep<T> where T : class
{
    public WhenStep(GivenStep<T> previousStep, Action<T> action)
    {
        PreviousStep = previousStep;
        previousStep.NextStep = this;
        
        StepDescription = action.Method.Name;
        TestStepFunction = value =>
        {
            action(value);
            return value;
        };
    }
    
    public WhenStep(GivenStep<T> previousStep, Func<T,T> manipulationFunction)
    {
        PreviousStep = previousStep;
        previousStep.NextStep = this;
        
        StepDescription = manipulationFunction.Method.Name;
        TestStepFunction = manipulationFunction;
    }
    
    public ShouldStep<T> Should() => new (this);
}