# FluentTests
An exploratory repo for looking into a way of creating "fluent" tests, inspired by FluentAssertions, but taking a similar approach to unit tests themselves


## What is the goal?
Would like to be able to create tests where the entire test name is constructed from the test "steps" and that these steps are broken down into small enough chunks that they can be human readable

### For example
Given("ABC").When(Reverse).Should().Be("CBA");

Which would generate the test name:
- Given(ABC)_When(Reverse)_Should_Be(CBA)
