using FluentTests.Steps;

namespace FluentTests;

public static class FluentTestStaticMethods
{
    public static GivenStep<T> Given<T>(Func<T> value) where T : class
    {
        return new GivenStep<T>(value.Method.Name, value);
    }
    
    public static GivenStep<T> Given<T>(T value) where T : class
    {
        return new GivenStep<T>(value.ToString(), () => value);
    }
}