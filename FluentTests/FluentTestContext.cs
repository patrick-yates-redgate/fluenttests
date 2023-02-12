using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace FluentTests;

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

public class FluentTestContext<TIn, TOut> : FluentTestContextBase
{
    protected FluentTestContext(FluentTest? fluentTest, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(fluentTest, stepName, stepContentsDescription)
    {
        InvokeTestSteps = testSteps;
    }

    public Func<TIn, TOut>? InvokeTestSteps { get; set; }

    public Func<TIn, TOut> AddStep(Func<TOut, TOut> func) => value => func(InvokeTestSteps!(value));
    public Func<TIn, TNewOut> AddStep<TNewOut>(Func<TOut, TNewOut> func) => value => func(InvokeTestSteps!(value));

    public Func<TIn, TOut> AddStep(Action<TOut> action) => value =>
    {
        var newValue = InvokeTestSteps!(value);
        action(newValue);
        return newValue;
    };

    public override void InvokeTest()
    {
        InvokeTestSteps!(default!);
    }
}

public abstract class FluentTestContext<TContext, TIn, TOut> : FluentTestContext<TIn, TOut>,
    IFluentTestContext<TContext>
    where TContext : FluentTestContext<TContext, TIn, TOut>
{
    protected FluentTestContext(FluentTest? fluentTest, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(fluentTest, testSteps, stepName, stepContentsDescription)
    {
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
    public FluentTestContextArrange(Func<TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(null, _ => testSteps(),
        stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextArrange(FluentTestContextBase? context, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context?.FluentTest, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public override FluentTestContextArrange<TIn, TOut> GetThis() => this;

    public FluentTestContextAction<TIn, TOut> When(Action<TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TOut> When(Func<TOut, TOut> stepFunction) =>
        When(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TOut> When(string stepContentsDescription,
        Action<TOut> stepFunction) =>
        new(this, AddStep(stepFunction), "When", stepContentsDescription);

    public FluentTestContextAction<TIn, TNewOut> When<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) =>
        new(this, AddStep(stepFunction), "When", stepContentsDescription);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(
        Func<TOut, TNewOut> stepFunction) => Then(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);
}

public class FluentTestContextAction<TIn, TOut> :
    FluentTestContext<FluentTestContextAction<TIn, TOut>, TIn, TOut>,
    IFluentTestContextAction<FluentTestContextAction<TIn, TOut>>
{
    public FluentTestContextAction(FluentTestContextBase? context, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context?.FluentTest, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public override FluentTestContextAction<TIn, TOut> GetThis() => this;
    
    public FluentTestContextAction<TIn, TOut> Then(string stepContentsDescription,
        Action<TOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);
    
    public FluentTestContextAction<TIn, TOut> Then(string stepContentsDescription,
        Func<TOut, TOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(
        Func<TOut, TNewOut> stepFunction) => Then(stepFunction.Method.Name, stepFunction);

    public FluentTestContextAction<TIn, TNewOut> Then<TNewOut>(string stepContentsDescription,
        Func<TOut, TNewOut> stepFunction) => new(this, AddStep(stepFunction), "Then", stepContentsDescription);
    
    public FluentTestContextArrange<TIn, TOut> And => new(this, InvokeTestSteps!, "And");
}

public class FluentTestContextAssertion<TIn, TOut> :
    FluentTestContext<FluentTestContextAssertion<TIn, TOut>, TIn, TOut>,
    IFluentTestContextAssertion<FluentTestContextAssertion<TIn, TOut>>
{
    public override FluentTestContextAssertion<TIn, TOut> GetThis() => this;

    public FluentTestContextAssertion(FluentTestContextBase? context, Func<TIn, TOut> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context?.FluentTest, testSteps, stepName,
        stepContentsDescription)
    {
    }
}

public class
    FluentTestContextAssertionNumeric<TIn, TNumeric> : FluentTestContextAssertion<TIn, NumericAssertions<TNumeric>>
    where TNumeric : struct, IComparable<TNumeric>
{
    public FluentTestContextAssertionNumeric(FluentTestContextBase? context,
        Func<TIn, NumericAssertions<TNumeric>> testSteps,
        string stepName, string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> Be(TNumeric expectation) =>
        new(this, AddStep(should => should.Be(expectation)), "Be", expectation.ToString());

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BeGreaterThan(TNumeric expectation) =>
        new(this, AddStep(should => should.BeGreaterThan(expectation)), "BeGreaterThan", expectation.ToString());

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BeNegative() =>
        new(this, AddStep(should => should.BeNegative()), "BeNegative");

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BePositive() =>
        new(this, AddStep(should => should.BePositive()), "BePositive");

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BeInRange(TNumeric minExpectation,
        TNumeric maxExpectation) =>
        new(this, AddStep(should => should.BeInRange(minExpectation, maxExpectation)), "BeInRange",
            $@"[{minExpectation}-{maxExpectation}]");

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BeLessThan(TNumeric expectation) =>
        new(this, AddStep(should => should.BeLessThan(expectation)), "BeLessThan", expectation.ToString());

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BeOneOf(params TNumeric[] expectation) =>
        new(this, AddStep(should => should.BeOneOf(expectation)), "BeOneOf", string.Join(',', expectation));

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BeGreaterOrEqualTo(TNumeric expectation) =>
        new(this, AddStep(should => should.BeGreaterOrEqualTo(expectation)), "BeGreaterOrEqualTo",
            expectation.ToString());

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> BeLessThanOrEqualTo(TNumeric expectation) =>
        new(this, AddStep(should => should.BeLessThanOrEqualTo(expectation)), "BeLessThanOrEqualTo",
            expectation.ToString());

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> NotBe(TNumeric expectation) =>
        new(this, AddStep(should => should.NotBe(expectation)), "NotBe", expectation.ToString());

    public FluentTestContextAssertionNumericAnd<TIn, TNumeric> NotBeInRange(TNumeric minExpectation,
        TNumeric maxExpectation) =>
        new(this, AddStep(should => should.NotBeInRange(minExpectation, maxExpectation)), "NotBeInRange",
            $@"[{minExpectation}-{maxExpectation}]");
}

public class
    FluentTestContextAssertionNumericAnd<TIn, TNumeric> : FluentTestContextAssertion<TIn,
        AndConstraint<NumericAssertions<TNumeric>>>
    where TNumeric : struct, IComparable<TNumeric>
{
    public FluentTestContextAssertionNumericAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<NumericAssertions<TNumeric>>> testSteps,
        string stepName, string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionNumeric<TIn, TNumeric> And() =>
        new(this, AddStep(and => and.And), "And");
}

public class FluentTestContextAssertionObject<TIn, TOut> : FluentTestContextAssertion<TIn, ObjectAssertions>
{
    public FluentTestContextAssertionObject(FluentTestContextBase? context, Func<TIn, ObjectAssertions> testSteps,
        string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionObjectAnd<TIn, TOut> Be(TOut expectation) =>
        new(this, AddStep(should => should.Be(expectation)), "Be", expectation.ToString());
    public FluentTestContextAssertionObjectAnd<TIn, TOut> Be(Func<TOut> expectation) =>
        new(this, AddStep(should => should.Be(expectation())), "Be", expectation.Method.Name);
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBe(TOut expectation) =>
        new(this, AddStep(should => should.NotBe(expectation)), "NotBe", expectation.ToString());
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBe(Func<TOut> expectation) =>
        new(this, AddStep(should => should.NotBe(expectation())), "NotBe", expectation.Method.Name);
    public FluentTestContextAssertionObjectAnd<TIn, TOut> BeEquivalentTo(TOut expectation) =>
        new(this, AddStep(should => should.BeEquivalentTo(expectation)), "Be", expectation.ToString());
    public FluentTestContextAssertionObjectAnd<TIn, TOut> BeEquivalentTo(Func<TOut> expectation) =>
        new(this, AddStep(should => should.BeEquivalentTo(expectation())), "BeEquivalentTo", expectation.Method.Name);
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBeEquivalentTo(TOut expectation) =>
        new(this, AddStep(should => should.NotBeEquivalentTo(expectation)), "NotBeEquivalentTo", expectation.ToString());
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBeEquivalentTo(Func<TOut> expectation) =>
        new(this, AddStep(should => should.NotBeEquivalentTo(expectation())), "NotBeEquivalentTo", expectation.Method.Name);
    public FluentTestContextAssertionObjectAnd<TIn, TOut> BeNull() =>
        new(this, AddStep(should => should.BeNull()), "BeNull");
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBeNull() =>
        new(this, AddStep(should => should.NotBeNull()), "NotBeNull");
    public FluentTestContextAssertionObjectAnd<TIn, TOut> BeSameAs(TOut expectation) =>
        new(this, AddStep(should => should.BeSameAs(expectation)), "BeSameAs", expectation.ToString());
    public FluentTestContextAssertionObjectAnd<TIn, TOut> BeSameAs(Func<TOut> expectation) =>
        new(this, AddStep(should => should.BeSameAs(expectation())), "BeSameAs", expectation.Method.Name);
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBeSameAs(TOut expectation) =>
        new(this, AddStep(should => should.NotBeSameAs(expectation)), "NotBeSameAs", expectation.ToString());
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBeSameAs(Func<TOut> expectation) =>
        new(this, AddStep(should => should.NotBeSameAs(expectation())), "NotBeSameAs", expectation.Method.Name);
    public FluentTestContextAssertionObjectAnd<TIn, TOut> BeOfType(Type expectedType) =>
        new(this, AddStep(should => should.BeOfType(expectedType)), "BeOfType", expectedType.Name);
    public FluentTestContextAssertionObjectAnd<TIn, TOut> NotBeOfType(Type expectedType) =>
        new(this, AddStep(should => should.NotBeOfType(expectedType)), "NotBeOfType", expectedType.Name);

    /*
    public FluentTestContextAssertionObject<TIn, TOut> Throw(Exception expectedException)
    {
        Func<TIn, ObjectAssertions> act = value =>
        {
            var shouldAct = Action() =>
            {

            };
            return InvokeTestSteps!(value);
        };
        
        

        return new(this, Action, "NotBeOfType", expectedType.Name);
    }
    */
}

public class
    FluentTestContextAssertionObjectAnd<TIn, TOut> : FluentTestContextAssertion<TIn, AndConstraint<ObjectAssertions>>
{
    public FluentTestContextAssertionObjectAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<ObjectAssertions>> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionObject<TIn, TOut> And =>
        new(this, AddStep(and => and.And), "And");
}

public class FluentTestContextAssertionString<TIn> : FluentTestContextAssertion<TIn, StringAssertions>
{
    public FluentTestContextAssertionString(FluentTestContextBase? context, Func<TIn, StringAssertions> testSteps,
        string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionStringAnd<TIn> Be(string value) =>
        new(this, AddStep(should => should.Be(value)), "Be", value.ToString());
}

public class FluentTestContextAssertionStringAnd<TIn> : FluentTestContextAssertion<TIn, AndConstraint<StringAssertions>>
{
    public FluentTestContextAssertionStringAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<StringAssertions>> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionString<TIn> And() =>
        new(this, AddStep(and => and.And), "And");
}

public class FluentTestContextAssertionBoolean<TIn> : FluentTestContextAssertion<TIn, BooleanAssertions>
{
    public FluentTestContextAssertionBoolean(FluentTestContextBase? context, Func<TIn, BooleanAssertions> testSteps,
        string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionBooleanAnd<TIn> Be(bool value) =>
        new(this, AddStep(should => should.Be(value)), "Be", value.ToString());
    public FluentTestContextAssertionBooleanAnd<TIn> BeTrue() =>
        new(this, AddStep(should => should.BeTrue()), "BeTrue");
    public FluentTestContextAssertionBooleanAnd<TIn> BeFalse() =>
        new(this, AddStep(should => should.BeFalse()), "BeFalse");
}

public class FluentTestContextAssertionBooleanAnd<TIn> : FluentTestContextAssertion<TIn, AndConstraint<BooleanAssertions>>
{
    public FluentTestContextAssertionBooleanAnd(FluentTestContextBase? context,
        Func<TIn, AndConstraint<BooleanAssertions>> testSteps, string stepName,
        string? stepContentsDescription = null) : base(context, testSteps, stepName,
        stepContentsDescription)
    {
    }

    public FluentTestContextAssertionBoolean<TIn> And() =>
        new(this, AddStep(and => and.And), "And");
}

public static class FluentTestContextExtensions
{
    #region REGION_SPECIAL_CASE_INT
    public static FluentTestContextAssertionNumeric<TIn, TNumeric> Should<TIn, TNumeric>(
        this FluentTestContextArrange<TIn, TNumeric> context) where TNumeric : struct, IComparable<TNumeric> =>
        new(context, context.AddStep(value => new NumericAssertions<TNumeric>(value)), "Should");
    
    public static FluentTestContextAssertionNumeric<TIn, TNumeric> Should<TIn, TNumeric>(
        this FluentTestContextAction<TIn, TNumeric> context) where TNumeric : struct, IComparable<TNumeric> =>
        new(context, context.AddStep(value => new NumericAssertions<TNumeric>(value)), "Should");
    #endregion
    
    #region REGION_SPECIAL_CASE_STRING
    public static FluentTestContextAssertionString<TIn> Should<TIn>(
        this FluentTestContextArrange<TIn, string> context) =>
        new(context, context.AddStep(value => value.Should()), "Should");
    
    public static FluentTestContextAssertionString<TIn> Should<TIn>(
        this FluentTestContextAction<TIn, string> context) =>
        new(context, context.AddStep(value => value.Should()), "Should");
    #endregion

    #region REGION_SPECIAL_CASE_OBJECT
    public static FluentTestContextAssertionObject<TIn, TIn> Should<TIn>(
        this FluentTestContextArrange<TIn, TIn> context) where TIn : class =>
        new(context, context.AddStep(value => new ObjectAssertions(value)), "Should");
    
    public static FluentTestContextAssertionObject<TIn, TOut> Should<TIn, TOut>(
        // I hate defaultValue usage here, but it differentiates this from the TNumeric case: https://stackoverflow.com/a/36775837
        this FluentTestContextAction<TIn, TOut> context, TOut defaultValue = default!) where TIn : class where TOut : class =>
        new(context, context.AddStep(value => new ObjectAssertions(value)), "Should");
    #endregion

    #region REGION_SPECIAL_CASE_BOOL
    public static FluentTestContextAssertionBoolean<TIn> Should<TIn>(
        this FluentTestContextArrange<TIn, bool> context) =>
        new(context, context.AddStep(value => new BooleanAssertions(value)), "Should");
    
    public static FluentTestContextAssertionBoolean<TIn> Should<TIn>(
        this FluentTestContextAction<TIn, bool> context) =>
        new(context, context.AddStep(value => new BooleanAssertions(value)), "Should");
    #endregion
}