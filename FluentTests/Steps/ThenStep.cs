namespace FluentTests.Steps;

public class ThenStep<T, TOut> : FluentTestStep<T, TOut>
{
    public ThenStep(FluentTestStep? previousStep, Func<T, TOut> transformFunc, string? stepDescription = null)
    {
        PreviousStep = previousStep;
        if (previousStep != null) previousStep.NextStep = this;
        
        TestStepFunction = transformFunc;
        StepDescription = stepDescription ?? transformFunc.Method.Name;
    }
    
    public ShouldStep<TOut> Should() => new (this);
}