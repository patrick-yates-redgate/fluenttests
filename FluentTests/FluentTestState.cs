using FluentAssertions.Primitives;

namespace FluentTests;

public class FluentTestState<TContext> : FluentTestState<TContext, TContext> where TContext : class
{
}

public class FluentTestState<TContextIn, TContextOut> where TContextIn : class where TContextOut : class
{
    public Func<TContextIn, TContextOut>? ExecuteTestStep { get; set; }
    public string? TestCaseName { get; set; }
    public TContextIn InitialValue { get; set; }

    public void ExecuteTest() => ExecuteTestStep(InitialValue);
}