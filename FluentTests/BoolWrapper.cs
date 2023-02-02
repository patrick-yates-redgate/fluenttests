namespace FluentTests;

public record BoolWrapper(bool Value)
{
    public override string ToString() => Value.ToString();
}