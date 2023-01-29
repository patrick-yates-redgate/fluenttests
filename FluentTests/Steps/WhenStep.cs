namespace FluentTests.Steps;

public class WhenStep<T> : FluentTestStep<T, T> where T : class
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

    public WhenStep(GivenStep<T> previousStep, Func<T, T> manipulationFunction)
    {
        PreviousStep = previousStep;
        previousStep.NextStep = this;

        StepDescription = manipulationFunction.Method.Name;
        TestStepFunction = manipulationFunction;
    }

    public ShouldStep<T> Should() => new(this);

    public ThenStep<T, TOut> Then<TOut>(Func<T, TOut> transformFunc) where TOut : class =>
        new(this, transformFunc);

    public ThenStep<T, NumberWrapperInt> Then(Func<T, int> transformFunc) =>
        new(this, value => new NumberWrapperInt(transformFunc(value)));

    public ThenStep<T, NumberWrapperFloat> Then(Func<T, float> transformFunc) =>
        new(this, value => new NumberWrapperFloat(transformFunc(value)));
}