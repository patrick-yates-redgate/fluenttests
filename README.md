# FluentTests
An exploratory repo for looking into a way of creating "fluent" tests, inspired by [FluentAssertions](https://github.com/fluentassertions/fluentassertions), but taking a similar approach to unit tests themselves

## What is the goal?
Would like to be able to create tests where the entire test name is constructed from the test "steps" and that these steps are broken down into small enough chunks that they can be human readable

### For example
Given("ABC").When(Reverse).Should().Be("CBA");

Which would generate the test name:
- Given(ABC)_When(Reverse)_Should_Be(CBA)

### Trouble Shooting
Until it is packaged properly you may find you get this if you import the library but don't also reference FluentAssertions. Adding FluentAssertions 6.9.0 via NuGet is the easiest way to fix

![image](https://user-images.githubusercontent.com/114094360/216642463-52f6f0e0-e46d-4542-8f17-0ee68b78b572.png)

### Contributions
Ideas, code, suggestions all most welcome :)
