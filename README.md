# fluent-tests
An exploratory repo for looking into a way of creating "fluent" tests, inspired by FluentAssertions, but taking a similar approach to unit tests themselves


## What is the goal?
Would like to be able to create tests where the entire test name is constructed from the test "steps" and that these steps are broken down into small enough chunks that they can be human readable

### For example
Given(Input(1)).When(Add(2)).Should(Have(3));

Which would generate the test name:
- GivenInput1_WhenAdd2_ShouldHave3

These could be outputted as folders so related tests are grouped together. I.e.
- Given1
  - WhenAdd2
    - ShouldHave3
  - WhenMul10
    - ShouldHave10
- Given2
  - WhenAdd2
    - ShouldHave4
  - WhenMul10
    - ShouldHave20
