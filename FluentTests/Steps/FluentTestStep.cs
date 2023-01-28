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

public abstract class FluentTestStep<T> : FluentTestStep where T : class
{
    public Func<T,T> TestStepFunction { get; set; }

    public override void InvokeTest()
    {
        if (PreviousStep is FluentTestStep<T>)
        {
            (PreviousStep as FluentTestStep<T>).InvokeTest();
            return;
        }

        var value = TestStepFunction(default(T));
        var testStep = this;
        while (testStep.NextStep is FluentTestStep<T>)
        {
            testStep = testStep.NextStep as FluentTestStep<T>;
            value = testStep.TestStepFunction(value);
        }
    }
}

public abstract class FluentTestStep<T, V> : FluentTestStep where T : class
    where V : class
{
    
}