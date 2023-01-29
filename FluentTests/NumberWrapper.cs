namespace FluentTests;

public record NumberWrapperInt(int Value)
{
    public override string ToString() => Value.ToString();
}

public record NumberWrapperFloat(float Value)
{
    public override string ToString() => Value.ToString();
}