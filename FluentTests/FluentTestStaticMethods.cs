using FluentTests.Steps;

namespace FluentTests;

public static class FluentTestStaticMethods
{
    public static GivenStep<T> Given<T>(Func<T> value) where T : class
    {
        return new GivenStep<T>(value);
    }
    
    public static GivenStep<T> Given<T>(T value) where T : class
    {
        return new GivenStep<T>(() => value, value?.ToString() ?? "null");
    }
    
    public static GivenStep<NumberWrapperInt> Given(int value)
    {
        return new GivenStep<NumberWrapperInt>(() => new NumberWrapperInt(value), value.ToString());
    }
    
    public static GivenStep<NumberWrapperFloat> Given(float value)
    {
        return new GivenStep<NumberWrapperFloat>(() => new NumberWrapperFloat(value), value.ToString());
    }
}