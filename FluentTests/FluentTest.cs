using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;
using FluentTests.Context;
using NUnit.Framework.Internal;

namespace FluentTests;

public static class FluentTests
{
    public static FluentTestContextArrange<T, T> Given<T>(string stepContentsDescription, Func<T> valueFunc) =>
        FluentTestContextArrange<T, T>.Given(stepContentsDescription, valueFunc);

    public static FluentTestContextArrange<T, T> Given<T>(Func<T> valueFunc) =>
        FluentTestContextArrange<T, T>.Given(valueFunc);
    
    public static FluentTestContextArrange<T, T> Given<T, TParam1>(Func<TParam1, T> valueFunc, TParam1 param1) =>
        FluentTestContextArrange<T, T>.Given(valueFunc, param1);
    
    public static FluentTestContextArrange<T, T> Given<T, TParam1, TParam2>(Func<TParam1, TParam2, T> valueFunc, TParam1 param1, TParam2 param2) =>
        FluentTestContextArrange<T, T>.Given(valueFunc, param1, param2);

    public static FluentTestContextArrange<T, T> Given<T>(T value) => FluentTestContextArrange<T, T>.Given(value);
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