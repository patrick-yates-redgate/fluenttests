using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace FluentTests;

public class FluentTestShouldState<TContextIn> where TContextIn : class
{
    public Func<TContextIn, ObjectAssertions>? ExecuteTest { get; set; }
    public string? TestCaseName { get; set; }
    public TContextIn InitialValue { get; set; }
}

public class FluentTestShouldStateInt<TContextIn> where TContextIn : class
{
    public Func<TContextIn, NumericAssertions<int>>? ExecuteTest { get; set; }
    public string? TestCaseName { get; set; }
    public TContextIn InitialValue { get; set; }
}