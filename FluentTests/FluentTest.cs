using System.Reflection;
using System.Reflection.Emit;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;
using FluentTests.Steps;
using NUnit.Framework.Internal;

namespace FluentTests;

public static class FluentTests
{
    public static FluentTestContextArrange<T, T> Given<T>(string stepContentsDescription, Func<T> valueFunc) =>
        new FluentTestContextArrange<T, T>(new FluentTest()).AddTestStep(new FluentTestStep<T>
            { StepName = "Given", StepContentsDescription = stepContentsDescription });

    public static FluentTestContextArrange<T, T> Given<T>(Func<T> valueFunc) => Given(valueFunc.Method.Name, valueFunc);
    public static FluentTestContextArrange<T, T> Given<T>(T value) => Given(value.ToString(), () => value);
}

public class FluentTest
{
    public List<FluentTestStep> TestSteps { get; set; } = new();

    public IEnumerable<string> NameParts => TestSteps.Select(step => step.TestStepName);

    public void AddTestStep(FluentTestStep fluentTestStep) => TestSteps.Add(fluentTestStep);

    public void InvokeTest()
    {
        FluentTestStep? previousStep = null;
        foreach (var step in TestSteps)
        {
        }
    }
}

public abstract class FluentTestStep
{
    public Type? InType { get; protected set; }
    public Type? OutType { get; protected set; }
    public string? StepName { get; set; }
    public string? StepContentsDescription { get; set; }
    
    public string? TestStepName
    {
        get => StepContentsDescription == null
            ? StepName
            : StepName + "(" + (string.IsNullOrEmpty(StepContentsDescription) ? "Empty" : StepContentsDescription) +
              ")";
    }

    //public abstract Action<FluentTestStep> InvokeTestStep();
}

public interface IFluentTestStepIn<in TIn>
{
}

public class FluentTestStep<TIn, TOut> : FluentTestStep, IFluentTestStepIn<TIn>
{
    public FluentTestStep<TIn, TOut> SetStepFunction(Func<TIn, TOut> stepFunc)
    {
        InType = typeof(TIn);
        OutType = typeof(TOut);
        StepFunction = stepFunc;
        return this;
    }
    
    public Func<TIn, TOut>? StepFunction { get; protected set; }

    public Action<TIn> BuildChain(Action<TOut> nextStep)
    {
        return value => nextStep(StepFunction(value));
    }
}

public class FluentTestStep<T> : FluentTestStep<T,T>
{
    public virtual FluentTestStep<T> SetStepFunction(Action<T> stepFunc)
    {
        InType = typeof(T);
        StepFunction = value =>
        {
            stepFunc(value);
            return value;
        };
        return this;
    }
    
    public virtual FluentTestStep<T> SetStepFunction(Func<T> stepFunc)
    {
        OutType = typeof(T);
        StepFunction = _ => stepFunc();
        return this;
    }
}