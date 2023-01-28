namespace FluentTests.Steps;

public abstract class FluentTestStep
{
    public string StepPrefix()
    {
        var name = GetType().Name;
        if (name.EndsWith("Step`1"))
        {
            name = name.Substring(0, name.Length - 6);
        }
        
        return name;
    }

    public string StepDescription { get; set; }

    public FluentTestStep NextStep { get; set; }
    
    public FluentTestStep PreviousStep { get; set; }

    public List<string> NameParts
    {
        get
        {
            var fullStepDescription = StepPrefix() + "(" + (string.IsNullOrWhiteSpace(StepDescription) ? string.Empty : StepDescription) + ")";

            var parts = PreviousStep?.NameParts ?? new List<string>();
            parts.Add(fullStepDescription);
            return parts;
        }
    }

    public abstract void InvokeTest();
}

public interface IFluentTestStepIn<T> where T : class
{
    void InvokeTestStep(T inValue);
}

public abstract class FluentTestStep<T, V> : FluentTestStep, IFluentTestStepIn<T> where T : class
    where V : class
{
    public Func<T,V> TestStepFunction { get; set; }

    public override void InvokeTest()
    {
        if (PreviousStep is FluentTestStep<T, V>)
        {
            (PreviousStep as FluentTestStep<T, V>).InvokeTest();
            return;
        }

        InvokeTestStep(default(T));
    }
    
    public void InvokeTestStep(T inValue)
    {
        var outValue = TestStepFunction(inValue);
        var testStep = this;
        if (testStep.NextStep is IFluentTestStepIn<V>)
        {
            (testStep.NextStep as IFluentTestStepIn<V>).InvokeTestStep(outValue);
        }
    }
}