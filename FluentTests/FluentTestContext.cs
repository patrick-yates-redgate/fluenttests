using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace FluentTests;

public class FluentTestContext<TIn, TOut>
{
    public FluentTest FluentTest { get; }

    protected FluentTestContext(FluentTest? fluentTest)
    {
        FluentTest = fluentTest ?? new FluentTest();
    }

    public List<FluentTestStep> TestSteps => FluentTest.TestSteps;
    public IEnumerable<string> NameParts => FluentTest.NameParts;

    public void InvokeTest() => FluentTest.InvokeTest();

    public Func<TIn, TOut>? InvokeTestSteps { get; set; }
}

public abstract class FluentTestContext<TContext, TIn, TOut> : FluentTestContext<TIn, TOut>,
    IFluentTestContext<TContext>
    where TContext : FluentTestContext<TContext, TIn, TOut>
{
    protected FluentTestContext(FluentTest? fluentTest) : base(fluentTest)
    {
    }

    //Here keep context
    public TContext AddTestStep(FluentTestStep<TOut> fluentTestStep)
    {
        FluentTest.AddTestStep(fluentTestStep);
        return GetThis();
    }

    //Here new context
    public TContext AddTestStep<TNewOut>(FluentTestStep<TOut, TNewOut> fluentTestStep)
    {
        FluentTest.AddTestStep(fluentTestStep);
        return GetThis();
    }

    public abstract TContext GetThis();
}

public interface IFluentTestContext<out TContext>
{
    TContext GetThis();
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

public class FluentTestContextArrange<TIn, TOut> :
    FluentTestContext<FluentTestContextArrange<TIn, TOut>, TIn, TOut>,
    IFluentTestContextArrange<FluentTestContextArrange<TIn, TOut>>
{
    public FluentTestContextArrange(FluentTest? fluentTest) : base(fluentTest)
    {
    }

    public override FluentTestContextArrange<TIn, TOut> GetThis() => this;

    public FluentTestContextAction<TIn, TOut> When(Action<TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TOut> When(Func<TOut, TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TOut> When(string stepContentsDescription,
        Action<TOut> stepFunction) => new FluentTestContextAction<TIn, TOut>(this, new FluentTestStep<TOut>
        { StepName = "When", StepContentsDescription = stepContentsDescription }, stepFunction);

    public FluentTestContextAction<TIn, TNewOut> When<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) => new FluentTestContextAction<TIn, TNewOut>(this, new FluentTestStep<TOut>
        { StepName = "When", StepContentsDescription = stepContentsDescription }, stepFunction);
}

public class FluentTestContextAction<TIn, TOut> :
    FluentTestContext<FluentTestContextAction<TIn, TOut>, TIn, TOut>,
    IFluentTestContextAction<FluentTestContextAction<TIn, TOut>>
{
    public FluentTestContextAction(FluentTest? fluentTest) : base(fluentTest)
    {
    }

    public override FluentTestContextAction<TIn, TOut> GetThis() => this;

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(
        Func<TOut, TNewOut> stepFunction) => Then(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) => Create(this, new FluentTestStep<TOut>
        { StepName = "Then", StepContentsDescription = stepContentsDescription }, stepFunction);


    /*
    public static FluentTestContextAction<TIn, TOut> Create(
        FluentTestContext<TIn, TOut> context, FluentTestStep<TOut> fluentTestStep,
        Action<TOut> stepFunction)
    {
        var newContext = new FluentTestContextAction<TIn, TOut>(context.FluentTest);
        newContext.TestSteps.Add(fluentTestStep);
        newContext.InvokeTestSteps = value =>
        {
            var newValue = context.InvokeTestSteps!(value);
            stepFunction(newValue);
            return newValue;
        };

        return newContext;
    }

    public static FluentTestContextAction<TIn, TNewOut> Create<TNewOut>(
        FluentTestContext<TIn, TOut> context, FluentTestStep<TOut> fluentTestStep,
        Func<TOut, TNewOut> stepFunction)
    {
        var newContext = new FluentTestContextAction<TIn, TNewOut>(context.FluentTest);
        newContext.TestSteps.Add(fluentTestStep);
        newContext.InvokeTestSteps = value => stepFunction(context.InvokeTestSteps!(value));

        return newContext;
    }*/
}

public class FluentTestContextAssertion<TIn, TOut> :
    FluentTestContext<FluentTestContextAssertion<TIn, TOut>, TIn, TOut>,
    IFluentTestContextAssertion<FluentTestContextAssertion<TIn, TOut>>
{
    public override FluentTestContextAssertion<TIn, TOut> GetThis() => this;

    public FluentTestContextAssertion(FluentTest? fluentTest) : base(fluentTest)
    {
    }

    /*
    public static FluentTestContextAssertion<TIn, TOut> Create(
        FluentTestContext<TIn, TOut> context, FluentTestStep<TOut> fluentTestStep,
        Action<TOut> stepFunction)
    {
        var newContext = new FluentTestContextAssertion<TIn, TOut>(context.FluentTest);
        newContext.TestSteps.Add(fluentTestStep);
        newContext.InvokeTestSteps = value =>
        {
            var newValue = context.InvokeTestSteps!(value);
            stepFunction(newValue);
            return newValue;
        };

        return newContext;
    }

    public static FluentTestContextAssertion<TIn, TNewOut> Create<TNewOut>(
        FluentTestContext<TIn, TOut> context, FluentTestStep<TOut> fluentTestStep,
        Func<TOut, TNewOut> stepFunction)
    {
        var newContext = new FluentTestContextAssertion<TIn, TNewOut>(context.FluentTest);
        newContext.TestSteps.Add(fluentTestStep);
        newContext.InvokeTestSteps = value => stepFunction(context.InvokeTestSteps!(value));

        return newContext;
    }
    */
}

public class
    FluentTestContextAssertionNumeric<TIn, TNumeric> : FluentTestContextAssertion<TIn, NumericAssertions<TNumeric>>
    where TNumeric : struct, IComparable<TNumeric>
{
    public FluentTestContextAssertionNumeric(FluentTest fluentTest) : base(fluentTest)
    {
    }

    public FluentTestContextAssertionNumeric(FluentTestContext<TIn, TNumeric> context,
        FluentTestStep<NumericAssertions<TNumeric>> fluentTestStep) : base(context.FluentTest)
    {
        TestSteps.Add(fluentTestStep);
        InvokeTestSteps = value => fluentTestStep.StepFunction?.Invoke(context.InvokeTestSteps!(value))!;
    }

    public FluentTestContextAssertionNumeric<TIn, int> Be(int value) => new(this,
        new FluentTestStep<NumericAssertions<int>>
                { StepName = "Be", StepContentsDescription = value.ToString() }
            .SetStepFunction(should => { should.Be(value); }));
}

public class FluentTestContextAssertionObject<TIn, T> : FluentTestContextAssertion<TIn, ObjectAssertions>
    where T : class
{
    public FluentTestContextAssertionObject(FluentTest? fluentTest) : base(fluentTest)
    {
    }

    public FluentTestContextAssertionObject<TIn, T> Be(T value) =>
        new(AddTestStep(new FluentTestStep<ObjectAssertions>
            { StepName = "Be", StepContentsDescription = value.ToString() }));
}

public class FluentTestContextAssertionString<TIn> : FluentTestContextAssertion<TIn, StringAssertions>
{
    public FluentTestContextAssertionString<TIn> Be(string value) =>
        new(AddTestStep(new FluentTestStep<StringAssertions>() { StepName = "Be", StepContentsDescription = value }));
}

public static class FluentTestContextExtensions
{
    public static FluentTestContextAssertionString<TIn> Should<TIn>(
        this IFluentTestContextArrange<FluentTestContextArrange<TIn, string>> fluentArrangeString) =>

    public static FluentTestContextAssertionNumeric<TIn> Should<TIn>(
        this IFluentTestContextArrange<FluentTestContextArrange<TIn, int>> fluentArrangeInt) =>
        new(fluentArrangeInt.GetThis().AddTestStep(new FluentTestStep<int>
            { StepName = "Should" }));

    public static FluentTestContextAssertionString<TIn> Should<TIn>(
        this IFluentTestContextAction<FluentTestContextAction<TIn, string>> fluentActionString) =>
        new(fluentActionString.GetThis().AddTestStep(new FluentTestStep<string>
            { StepName = "Should" }));

    public static FluentTestContextAssertionNumeric<TIn> Should<TIn>(
        this IFluentTestContextAction<FluentTestContextAction<TIn, int>> fluentActionInt) =>
        new(fluentActionInt.GetThis().AddTestStep(new FluentTestStep<int>
            { StepName = "Should" }));
}