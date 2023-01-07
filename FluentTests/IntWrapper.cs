using FluentAssertions.Numeric;

namespace FluentTests;

public class IntWrapper
{
    public int Value { get; set; }

    public NumericAssertions<int> Should() => Value.Should();
}