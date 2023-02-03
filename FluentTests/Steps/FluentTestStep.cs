namespace FluentTests.Steps;

public abstract class FluentTestStep
{
    public virtual string? StepPrefix()
    {
        var name = GetType().Name;
        if (name.EndsWith("Step`1") || name.EndsWith("Step`2"))
        {
            name = name.Substring(0, name.Length - 6);
        }
        
        return name;
    }

    public string? StepDescription { get; set; }

    public FluentTestStep NextStep { get; set; }
    
    public FluentTestStep? PreviousStep { get; set; }

    public List<string> NameParts
    {
        get
        {
            var fullStepDescription = StepPrefix() + (string.IsNullOrWhiteSpace(StepDescription) ? string.Empty : "(" + StepDescription + ")");

            var parts = PreviousStep?.NameParts ?? new List<string>();
            parts.Add(fullStepDescription);
            return parts;
        }
    }

    public abstract void InvokeTest();
}

public interface IFluentTestStepIn<in T>
{
    void InvokeTestStep(T inValue);
}

public abstract class FluentTestStep<T, TOut> : FluentTestStep, IFluentTestStepIn<T>
{
    public Func<T, TOut> TestStepFunction { get; set; }

    public override void InvokeTest()
    {
        if (PreviousStep != null)
        {
            PreviousStep.InvokeTest();
            return;
        }

        InvokeTestStep(default(T));
    }
    
    public void InvokeTestStep(T inValue)
    {
        var outValue = TestStepFunction(inValue);
        var testStep = this;
        if (testStep.NextStep is IFluentTestStepIn<TOut>)
        {
            (testStep.NextStep as IFluentTestStepIn<TOut>).InvokeTestStep(outValue);
        }
    }
}