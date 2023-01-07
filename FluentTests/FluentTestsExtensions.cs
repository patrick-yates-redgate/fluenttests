using FluentAssertions.Primitives;

namespace FluentTests;

public static class FluentTestsExtensions
{
    public static FluentTestState<TContextIn, TContextOut> When<TContextIn, TContextOut>(this FluentTestState<TContextIn> contextIn,
        Func<TContextIn, TContextOut> doAction) where TContextIn : class where TContextOut : class
    {
        var conditionName = contextIn.TestCaseName + "_When" + doAction.Method.Name;
        var existingSteps = contextIn.ExecuteTestStep;
        
        /*
        var join = string.Empty;
        var conditionName = state.TestCaseName + "_Where";
        
        foreach (var conditionAction in conditionActions)
        {
            conditionName += join + conditionAction.Method.Name;
            join = "And";
        }
        */

        return new FluentTestState<TContextIn, TContextOut>
        {
            ExecuteTestStep = executeContext => doAction(existingSteps?.Invoke(contextIn.ExecuteTestStep?.Invoke(executeContext)!)!),
            TestCaseName = conditionName
        };
    }
    
    public static FluentTestState<TContextIn, IntWrapper> When<TContextIn>(this FluentTestState<TContextIn> contextIn,
        Func<TContextIn, int> doAction) where TContextIn : class
    {
        var conditionName = contextIn.TestCaseName + "_Where" + doAction.Method.Name;
        var existingSteps = contextIn.ExecuteTestStep;
        
        return new FluentTestState<TContextIn, IntWrapper>
        {
            ExecuteTestStep = executeContext => new IntWrapper { Value = doAction(existingSteps!(contextIn.ExecuteTestStep!(executeContext))) },
            TestCaseName = conditionName,
            InitialValue = contextIn.InitialValue
        };
    }

    public static FluentTestShouldState<TContextIn> Should<TContextIn, TContextOut>(this FluentTestState<TContextIn, TContextOut> contextIn)
        where TContextIn : class where TContextOut : class
    {
        var conditionName = contextIn.TestCaseName + "_Should";
        var existingSteps = contextIn.ExecuteTestStep;
        
        return new FluentTestShouldState<TContextIn>
        {
            ExecuteTest = executeContext => existingSteps!(executeContext).Should(),
            TestCaseName = conditionName,
            InitialValue = contextIn.InitialValue
        };
    }

    /*
    public static FluentTestShouldStateInt<TContextIn> Should<TContextIn, IntWrapper>(this FluentTestState<TContextIn, IntWrapper> contextIn)
        where TContextIn : class
    {
        var conditionName = contextIn.TestCaseName + "_Should";
        var existingSteps = contextIn.ExecuteTest;
        
        return new FluentTestShouldStateInt<TContextIn>
        {
            ExecuteTest = executeContext => existingSteps!(executeContext).Should(),
            TestCaseName = conditionName,
            InitialValue = contextIn.InitialValue
        };
    }*/

    public static FluentTestAssertionState<TContext, TContext, AndConstraint<ObjectAssertions>> Be<TContext>(this FluentTestShouldState<TContext> contextIn, TContext expectedValue)
        where TContext : class
    {
        var conditionName = contextIn.TestCaseName + "Be" + expectedValue;
        var existingSteps = contextIn.ExecuteTest;
        
        return new FluentTestAssertionState<TContext, TContext, AndConstraint<ObjectAssertions>>
        {
            ExecuteTest = executeContext => existingSteps!(executeContext).Be(expectedValue),
            TestCaseName = conditionName,
            InitialValue = contextIn.InitialValue
        };
    }
}