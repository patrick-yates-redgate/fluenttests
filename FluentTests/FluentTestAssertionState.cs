namespace FluentTests;

public class FluentTestAssertionState<TInitialValue, TContextIn, TContextOut> where TInitialValue : class where TContextIn : class where TContextOut : class
{
    public Func<TContextIn, TContextOut>? ExecuteTest { get; set; }
    public string? TestCaseName { get; set; }
    public TInitialValue InitialValue { get; set; }
}