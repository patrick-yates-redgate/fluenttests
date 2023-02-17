namespace FluentTests.Context;

public static class FluentTestContextAssertionNumericExtensions
{
    /*
    public static AndConstraint<NumericAssertions<float>> BeApproximately(this NumericAssertions<float> parent,
        float expectedValue, float precision, string because = "",
        params object[] becauseArgs)
        */
    public static FluentTestContextAssertionNumericAnd<TIn, float> BeApproximately<TIn>(this FluentTestContextAssertionNumeric<TIn, float> parent, float expectedValue, float precision, string because = "",
        params object[] becauseArgs) =>
        new(parent, parent.AddStep(should => should.BeApproximately(expectedValue, precision, because, becauseArgs)), "BeApproximately", expectedValue + "{p" + precision + "}");
    public static FluentTestContextAssertionNumericAnd<TIn, float> NotBeApproximately<TIn>(this FluentTestContextAssertionNumeric<TIn, float> parent, float expectedValue, float precision, string because = "",
        params object[] becauseArgs) =>
        new(parent, parent.AddStep(should => should.NotBeApproximately(expectedValue, precision, because, becauseArgs)), "NotBeApproximately", expectedValue + "{p" + precision + "}");
    
    public static FluentTestContextAssertionNumericAnd<TIn, double> BeApproximately<TIn>(this FluentTestContextAssertionNumeric<TIn, double> parent, double expectedValue, double precision, string because = "",
        params object[] becauseArgs) =>
        new(parent, parent.AddStep(should => should.BeApproximately(expectedValue, precision, because, becauseArgs)), "BeApproximately", expectedValue + "{p" + precision + "}");
    public static FluentTestContextAssertionNumericAnd<TIn, double> NotBeApproximately<TIn>(this FluentTestContextAssertionNumeric<TIn, double> parent, double expectedValue, double precision, string because = "",
        params object[] becauseArgs) =>
        new(parent, parent.AddStep(should => should.NotBeApproximately(expectedValue, precision, because, becauseArgs)), "NotBeApproximately", expectedValue + "{p" + precision + "}");
    
    public static FluentTestContextAssertionNumericAnd<TIn, decimal> BeApproximately<TIn>(this FluentTestContextAssertionNumeric<TIn, decimal> parent, decimal expectedValue, decimal precision, string because = "",
        params object[] becauseArgs) =>
        new(parent, parent.AddStep(should => should.BeApproximately(expectedValue, precision, because, becauseArgs)), "BeApproximately", expectedValue + "{p" + precision + "}");
    public static FluentTestContextAssertionNumericAnd<TIn, decimal> NotBeApproximately<TIn>(this FluentTestContextAssertionNumeric<TIn, decimal> parent, decimal expectedValue, decimal precision, string because = "",
        params object[] becauseArgs) =>
        new(parent, parent.AddStep(should => should.NotBeApproximately(expectedValue, precision, because, becauseArgs)), "NotBeApproximately", expectedValue + "{p" + precision + "}");
}