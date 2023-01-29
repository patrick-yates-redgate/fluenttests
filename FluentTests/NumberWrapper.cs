namespace FluentTests;

public record NumberWrapper(int Value)
{
    public override string ToString() => Value.ToString();
}