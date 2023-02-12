using System.Reflection;
using System.Reflection.Emit;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;
using NUnit.Framework.Internal;

namespace FluentTests;

public static class FluentTests
{
    public static FluentTestContextArrange<T, T> Given<T>(string stepContentsDescription, Func<T> valueFunc) =>
        new(null, _ => valueFunc(), "Given", stepContentsDescription);
    
    public static FluentTestContextArrange<T, T> Given<T>(Func<T> valueFunc) => Given(valueFunc.Method.Name, valueFunc);
    public static FluentTestContextArrange<T, T> Given<T>(T value) => Given(value == null ? "null" : value.ToString(), () => value);
}

public class FluentTest
{
    public List<FluentTestStep> TestSteps { get; set; } = new();

    public IEnumerable<string> NameParts => TestSteps.Select(step => step.TestStepName);
}

public class FluentTestStep
{
    public string? StepName { get; set; }
    public string? StepContentsDescription { get; set; }
    
    public string? TestStepName =>
        StepContentsDescription == null
            ? StepName
            : StepName + "(" + (string.IsNullOrEmpty(StepContentsDescription) ? "Empty" : StepContentsDescription) +
              ")";
}