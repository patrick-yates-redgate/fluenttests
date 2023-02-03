namespace FluentTests.Steps;

public class FluentTestFromShouldStep<T> : FluentTestStep<T, T>
{
    protected FluentTestFromShouldStep(FluentTestStep? previousStep, string? stepDescription = null)
    {
        StepDescription = stepDescription;
        PreviousStep = previousStep;
        if (previousStep != null) previousStep.NextStep = this;
    }

    protected FluentTestFromShouldStep(FluentTestStep? previousStep, T value, string? stepDescription = null)
        : this(previousStep, stepDescription)
    {
        StepDescription = stepDescription ?? value.ToString();
    }

    protected FluentTestFromShouldStep(FluentTestStep? previousStep, Action<T> action, string? stepDescription = null)
        : this(previousStep)
    {
        StepDescription = stepDescription ?? action.Method.Name;
        TestStepFunction = value =>
        {
            action(value);
            return value;
        };
    }

    protected FluentTestFromShouldStep(FluentTestStep? previousStep, Func<T> func, string? stepDescription = null)
        : this(previousStep)
    {
        StepDescription = stepDescription ?? func.Method.Name;
        TestStepFunction = _ => func();
    }
}