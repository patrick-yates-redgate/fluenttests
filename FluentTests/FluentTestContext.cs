using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace FluentTests;

public class FluentTestContext
{
    public FluentTest FluentTest { get; set; }

    protected FluentTestContext(FluentTest fluentTest)
    {
        FluentTest = fluentTest;
    }

    public List<FluentTestStep> TestSteps => FluentTest.TestSteps;
    public IEnumerable<string> NameParts => FluentTest.NameParts;

    public void InvokeTest() => FluentTest.InvokeTest();
}

public abstract class FluentTestContext<TContext> : FluentTestContext where TContext : FluentTestContext
{
    protected FluentTestContext(FluentTest fluentTest) : base(fluentTest)
    {
    }
}

public abstract class FluentTestContext<TContext, TReturnType> : FluentTestContext<TContext>, IFluentTestContext<TContext>
    where TContext : FluentTestContext<TContext, TReturnType>
{
    protected FluentTestContext(FluentTest fluentTest) : base(fluentTest)
    {
    }
    
    protected FluentTestContext(FluentTestContext context) : base(context.FluentTest)
    {
    }
    
    public TContext AddTestStep(FluentTestStep fluentTestStep)
    {
        FluentTest.AddTestStep(fluentTestStep);
        return GetThis();
    }

    public abstract TContext GetThis();

    public FluentTestContext Context() => this;
}

public interface IFluentTestContext<out TContext>
{
    TContext GetThis();
    
    FluentTestContext Context();
}

public interface IFluentTestContextArrange<out TContext> : IFluentTestContext<TContext>
{
}

public interface IFluentTestContextAction<out TContext> : IFluentTestContext<TContext>
{
}

public interface IFluentTestContextAssertion<out TContext> : IFluentTestContext<TContext>
{
}

public class FluentTestContextArrange<TIn> :
    FluentTestContext<FluentTestContextArrange<TIn>, TIn>, IFluentTestContextArrange<FluentTestContextArrange<TIn>>
{
    public FluentTestContextArrange(FluentTest fluentTest) : base(fluentTest)
    {
    }

    public override FluentTestContextArrange<TIn> GetThis() => this;
    
    public FluentTestContextAction<TIn> When(Action<TIn> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);
    
    public FluentTestContextAction<TIn> When(Func<TIn, TIn> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);
    
    public FluentTestContextAction<TIn> When(string stepContentsDescription,
        Action<TIn> stepFunction) =>
        new(AddTestStep(new FluentTestStep<TIn>
            { StepName = "When", StepContentsDescription = stepContentsDescription }.SetStepFunction(stepFunction)));

    public FluentTestContextAction<TOut> When<TOut>(string stepContentsDescription,
        Func<TIn, TOut> stepFunction) =>
        new(AddTestStep(new FluentTestStep<TIn>
            { StepName = "When", StepContentsDescription = stepContentsDescription }));
}

public class FluentTestContextAction<TIn> :
    FluentTestContext<FluentTestContextAction<TIn>, TIn>, IFluentTestContextAction<FluentTestContextAction<TIn>>
{
    public FluentTestContextAction(FluentTestContext context) : base(context)
    {
    }
    
    public override FluentTestContextAction<TIn> GetThis() => this;

    public FluentTestContextAction<TOut> Then<TOut>(
        Func<TIn, TOut> stepFunction) => Then(stepFunction.Method.Name, stepFunction);
    
    public FluentTestContextAction<TOut> Then<TOut>(string stepContentsDescription,
        Func<TIn, TOut> stepFunction) =>
        new(AddTestStep(new FluentTestStep<TIn>
            { StepName = "Then", StepContentsDescription = stepContentsDescription }));
}

public class FluentTestContextAssertion<TReturnType> :
    FluentTestContext<FluentTestContextAssertion<TReturnType>, TReturnType>, IFluentTestContextAssertion<FluentTestContextAssertion<TReturnType>>
{
    public FluentTestContextAssertion(FluentTestContext context) : base(context)
    {
    }

    public override FluentTestContextAssertion<TReturnType> GetThis() => this;
}

public class FluentTestContextAssertionNumeric : FluentTestContextAssertion<NumericAssertions<int>>
{
    public FluentTestContextAssertionNumeric(FluentTestContext context) : base(context)
    {
    }
}

public class FluentTestContextAssertionObject<T> : FluentTestContextAssertion<ObjectAssertions> where T : class
{
    public FluentTestContextAssertionObject(FluentTestContext context) : base(context)
    {
    }

    public FluentTestContextAssertionObject<T> Be(T value) => 
        new(AddTestStep(new FluentTestStep<object> { StepName = "Be", StepContentsDescription = value.ToString() }));
}

public class FluentTestContextAssertionString : FluentTestContextAssertion<NumericAssertions<int>>
{
    public FluentTestContextAssertionString(FluentTestContext context) : base(context)
    {
    }

    public FluentTestContextAssertionString Be(string value) => 
        new(AddTestStep(new FluentTestStep<string>() { StepName = "Be", StepContentsDescription = value }));
}

public static class FluentTestContextExtensions
{
    public static FluentTestContextAssertionString Should(this IFluentTestContextArrange<FluentTestContextArrange<string>> fluentArrangeString) =>
        new(fluentArrangeString.GetThis().AddTestStep(new FluentTestStep<string>
            { StepName = "Should" }));
    
    public static FluentTestContextAssertionNumeric Should(this IFluentTestContextArrange<FluentTestContextArrange<int>> fluentArrangeInt) =>
        new(fluentArrangeInt.GetThis().AddTestStep(new FluentTestStep<int>
            { StepName = "Should" }));
    
    public static FluentTestContextAssertionString Should(this IFluentTestContextAction<FluentTestContextAction<string>> fluentActionString) =>
        new(fluentActionString.GetThis().AddTestStep(new FluentTestStep<string>
            { StepName = "Should" }));
    
    public static FluentTestContextAssertionNumeric Should(this IFluentTestContextAction<FluentTestContextAction<int>> fluentActionInt) =>
        new(fluentActionInt.GetThis().AddTestStep(new FluentTestStep<int>
            { StepName = "Should" }));
}