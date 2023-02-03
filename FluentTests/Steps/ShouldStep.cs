using System.Linq.Expressions;
using FluentAssertions.Primitives;

namespace FluentTests.Steps;

public class ShouldStep<T> : FluentTestStep<T, T> where T : class
{
    public ShouldStep(FluentTestStep? previousStep)
    {
        PreviousStep = previousStep;
        previousStep.NextStep = this;
        TestStepFunction = value => value;
    }
    
    public BeStep<T> Be(T expectation) =>
        new(this, expectation, "Be", (should) => should.Be(expectation));
    
    public BeStep<T> NotBe(T expectation) =>
        new(this, expectation, "NotBe", (should) => should.NotBe(expectation));
    public BeStep<T> BeEquivalentTo(T expectation) =>
        new(this, expectation, "BeEquivalentTo", (should) => should.BeEquivalentTo(expectation));
    public BeStep<T> BeEquivalentTo(Func<T> expectation) =>
        new(this, expectation, "BeEquivalentTo", (should) => should.BeEquivalentTo(expectation()));
    public BeStep<T> NotBeEquivalentTo(T expectation) =>
        new(this, expectation, "NotBeEquivalentTo", (should) => should.NotBeEquivalentTo(expectation));
    public BeStep<T> BeNull() =>
        new(this, "BeNull", (should) => should.BeNull());
    public BeStep<T> NotBeNull() =>
        new(this, "NotBeNull", (should) => should.NotBeNull());
    public BeStep<T> BeSameAs(T expectation) =>
        new(this, expectation, "BeSameAs", (should) => should.BeSameAs(expectation));
    public BeStep<T> NotBeSameAs(T expectation) =>
        new(this, expectation, "NotBeSameAs", (should) => should.NotBeSameAs(expectation));

    public BeStep<T> BeOfType(Type expectedType) =>
        new(this, "BeOfType", (should) => should.BeOfType(expectedType), expectedType.Name);
    public BeStep<T> NotBeOfType(Type expectedType) =>
        new(this, "NotBeOfType", (should) => should.NotBeOfType(expectedType), expectedType.Name);
    public BeStep<T> Throw(Exception expectedException) =>
        new(this, "Throw", (should) => throw new NotImplementedException("Coming soon!"), expectedException.GetType().Name);

    #region REGION_SPECIAL_CASE_INT
    public BeStepInt Be(int expectation) =>
        new(this, expectation, "Be", (should) => should.Be(expectation));
    public BeStepInt BeGreaterThan(int expectation) =>
        new(this, expectation, "BeGreaterThan", (should) => should.BeGreaterThan(expectation));
    public BeStepInt BeNegative() =>
        new(this, "BeNegative", (should) => should.BeNegative());
    public BeStepInt BePositive() =>
        new(this, "BePositive", (should) => should.BePositive());
    public BeStepInt BeInRange(int minimumValue, int maximumValue) =>
        new(this, "BeInRange", (should) => should.BeInRange(minimumValue, maximumValue), $@"[{minimumValue}-{maximumValue}]");
    public BeStepInt BeLessThan(int expectation) =>
        new(this, expectation, "BeLessThan", (should) => should.BeLessThan(expectation));
    public BeStepInt BeOneOf(int expectation) =>
        new(this, expectation, "BeOneOf", (should) => should.BeOneOf(expectation));
    public BeStepInt BeGreaterOrEqualTo(int expectation) =>
        new(this, expectation, "BeGreaterOrEqualTo", (should) => should.BeGreaterOrEqualTo(expectation));
    public BeStepInt BeLessThanOrEqualTo(int expectation) =>
        new(this, expectation, "BeLessThanOrEqualTo", (should) => should.BeLessThanOrEqualTo(expectation));
    public BeStepInt NotBe(int expectation) =>
        new(this, expectation, "NotBe", (should) => should.NotBe(expectation));
    public BeStepInt NotBeInRange(int minimumValue, int maximumValue) =>
        new(this, "NotBeInRange", (should) => should.NotBeInRange(minimumValue, maximumValue), $@"[{minimumValue}-{maximumValue}]");
    #endregion
    
    #region REGION_SPECIAL_CASE_FLOAT
    public BeStepFloat Be(float expectation) =>
        new(this, expectation, "Be", (should) => should.Be(expectation));
    public BeStepFloat BeGreaterThan(float expectation) =>
        new(this, expectation, "BeGreaterThan", (should) => should.BeGreaterThan(expectation));
    public BeStepFloat BeInRange(float minimumValue, float maximumValue) =>
        new(this, "BeInRange", (should) => should.BeInRange(minimumValue, maximumValue), $@"[{minimumValue}-{maximumValue}]");
    public BeStepFloat BeLessThan(float expectation) =>
        new(this, expectation, "BeLessThan", (should) => should.BeLessThan(expectation));
    public BeStepFloat BeOneOf(float expectation) =>
        new(this, expectation, "BeOneOf", (should) => should.BeOneOf(expectation));
    public BeStepFloat BeGreaterOrEqualTo(float expectation) =>
        new(this, expectation, "BeGreaterOrEqualTo", (should) => should.BeGreaterOrEqualTo(expectation));
    public BeStepFloat BeLessThanOrEqualTo(float expectation) =>
        new(this, expectation, "BeLessThanOrEqualTo", (should) => should.BeLessThanOrEqualTo(expectation));
    public BeStepFloat NotBe(float expectation) =>
        new(this, expectation, "NotBe", (should) => should.NotBe(expectation));
    public BeStepFloat NotBeInRange(float minimumValue, float maximumValue) =>
        new(this, "NotBeInRange", (should) => should.NotBeInRange(minimumValue, maximumValue), $@"[{minimumValue}-{maximumValue}]");

    #endregion
    
    #region REGION_SPECIAL_CASE_BOOL
    public BeStepBool BeTrue() =>
        new(this, false, "Be", (should) => should.BeTrue());
    public BeStepBool BeFalse() =>
        new(this, false, "Be", (should) => should.BeFalse());

    #endregion
}