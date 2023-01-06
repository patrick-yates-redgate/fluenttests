namespace FluentTests;

public class FluentTests
{
    public static FluentTestState<TContext> Given<TContext>(params Action<TContext>[] conditionActions) where TContext : class
    {
        var join = string.Empty;
        var conditionName = "Given";
        
        foreach (var conditionAction in conditionActions)
        {
            conditionName += join + conditionAction.Method.Name;
            join = "And";
        }

        return new FluentTestState<TContext>
        {
            ExecuteTest = context =>
            {
                foreach (var conditionAction in conditionActions)
                {
                    conditionAction(context);
                }
            },
            TestCaseName = conditionName
        };
    }
}