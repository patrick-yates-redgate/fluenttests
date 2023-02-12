using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace FluentTests.Context;

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