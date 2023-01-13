using FluentAssertions.Primitives;

namespace FluentTests;

public static class FluentTestsExtensions
{
    public static FluentTestState<TContextIn, TContextOut> When<TContextIn, TContextOut>(
        this FluentTestState<TContextIn> contextIn,
        Func<TContextIn, TContextOut> doAction) where TContextIn : class where TContextOut : class
    {
        var conditionName = contextIn.TestName + "_When" + doAction.Method.Name;
        var existingSteps = contextIn.ExecuteTestStep;

        return new FluentTestState<TContextIn, TContextOut>
        {
            ExecuteTestStep = executeContext =>
                doAction(existingSteps?.Invoke(contextIn.ExecuteTestStep?.Invoke(executeContext)!)!),
            TestName = conditionName,
            InitialValue = contextIn.InitialValue
        };
    }

    public static FluentTestShouldState<TContextIn> Should<TContextIn, TContextOut>(
        this FluentTestState<TContextIn, TContextOut> contextIn)
        where TContextIn : class where TContextOut : class
    {
        var conditionName = contextIn.TestName + "_Should";
        var existingSteps = contextIn.ExecuteTestStep;

        return new FluentTestShouldState<TContextIn>
        {
            ExecuteTestStep = executeContext => existingSteps!(executeContext).Should(),
            TestName = conditionName,
            InitialValue = contextIn.InitialValue
        };
    }

    public static FluentTestAssertionState<TContext, TContext, AndConstraint<ObjectAssertions>> Be<TContext>(
        this FluentTestShouldState<TContext> contextIn, TContext expectedValue)
        where TContext : class
    {
        var conditionName = contextIn.TestName + "Be" + expectedValue;
        var existingSteps = contextIn.ExecuteTestStep;

        return new FluentTestAssertionState<TContext, TContext, AndConstraint<ObjectAssertions>>
        (
            conditionName,
            executeContext => existingSteps!(executeContext).Be(expectedValue),
            contextIn.InitialValue
        );
    }

    public static FluentTestAction ToFluentTestAction<TInitialValue, TContextOut>(
        this FluentTestAssertionState<TInitialValue, TInitialValue, TContextOut> state)
        where TInitialValue : class where TContextOut : class => new
        FluentTestAction
        {
            ExecuteTest = () => state.ExecuteTestStep(state.InitialValue)
        };

    public static FluentTestAction ToFluentTestAction<T>(this FluentTestShouldState<T> state) where T : class => new
        FluentTestAction
        {
            ExecuteTest = () => state.ExecuteTestStep!(state.InitialValue)
        };
}