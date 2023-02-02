namespace FluentTests.Examples.ClassTests;

public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public float Height { get; set; }

    public Person(string? name = null)
    {
        Name = name;
    }

    public void HaveBirthday()
    {
        ++Age;
    }
}