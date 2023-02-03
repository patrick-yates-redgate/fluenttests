using FluentTests.Steps;

namespace FluentTests;

public static class FluentTestStaticMethods
{
    public static GivenStep<T> Given<T>(Func<T> value)
    {
        return new GivenStep<T>(value);
    }
    
    public static GivenStep<T> Given<T>(T value)
    {
        return new GivenStep<T>(() => value, value is string { Length: 0 } ? "Empty" : value?.ToString() ?? "null");
    }
}