namespace FluentTests.Context;

public abstract class FluentTestContextBase
{
    public FluentTest FluentTest { get; }

    protected FluentTestContextBase(FluentTest? fluentTest, string stepName,
        string? stepContentsDescription = null)
    {
        FluentTest = fluentTest ?? new FluentTest();
        TestSteps.Add(new FluentTestStep { StepContentsDescription = stepContentsDescription, StepName = stepName });
    }

    public List<FluentTestStep> TestSteps => FluentTest.TestSteps;
    public IEnumerable<string> NameParts => FluentTest.NameParts;

    public abstract void InvokeTest();
}