namespace FluentTests;

public static class FluentTests
{
    public static FluentTestState<TContext> Given<TContext>(TContext initialValue,
        params Action<TContext>[] conditionActions) where TContext : class
    {
        var join = string.Empty;
        var conditionName = "Given";
        if (!string.IsNullOrWhiteSpace(initialValue?.ToString()))
        {
            conditionName += initialValue;
            join = "And";
        }

        foreach (var conditionAction in conditionActions)
        {
            conditionName += join + conditionAction.Method.Name;
            join = "And";
        }

        return new FluentTestState<TContext>
        {
            TestName = conditionName,
            ExecuteTestStep = context =>
            {
                foreach (var conditionAction in conditionActions)
                {
                    conditionAction(context);
                }

                return context;
            },
            InitialValue = initialValue
        };
    }
}