using FluentAssertions.Primitives;

namespace FluentTests.Context;

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