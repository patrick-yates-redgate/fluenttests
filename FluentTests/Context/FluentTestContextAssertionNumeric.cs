using FluentAssertions.Numeric;
using FluentAssertions.Specialized;

namespace FluentTests.Context;

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

    public FluentTestContextAssertionNumeric<TIn, TNumeric> Throw<TException>(string because = "")
        where TException : Exception =>
        new FluentTestContextAssertionNumeric<TIn, TNumeric>(this, AddStep(should => should), "Throw",
            typeof(TException).Name).WithActionAssertion(should => { should.Throw<TException>(because); });
    
    public FluentTestContextAssertionNumeric<TIn, TNumeric> NotThrow<TException>(string because = "")
        where TException : Exception =>
        new FluentTestContextAssertionNumeric<TIn, TNumeric>(this, AddStep(should => should), "NotThrow",
            typeof(TException).Name).WithActionAssertion(should => { should.NotThrow<TException>(because); });

    public FluentTestContextAssertionNumeric<TIn, TNumeric> WithActionAssertion(Action<ActionAssertions> act)
    {
        ActionAssertion = act;
        return this;
    }
}