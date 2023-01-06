namespace FluentTests;

public class FluentTestState<TContext> where TContext : class
{
    public Action<TContext>? ExecuteTest { get; set; }
    public string? TestCaseName { get; set; }
}