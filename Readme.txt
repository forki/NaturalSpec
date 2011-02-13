NaturalSpec is based on NUnit and completely written in F# - 
but you don't have to learn F# to use it.

== Tutorials ==

* http://bit.ly/aKmLMx - Introducing NaturalSpec
* http://bit.ly/rNIsm - "Getting started" with NaturalSpec
* http://bit.ly/aBS5db - Using NaturalSpec to create a spec for C# projects
* http://bit.ly/8ZtTTe - Mocking objects with NaturalSpec
* http://bit.ly/aqDrUj - Parameterized Scenarios with NaturalSpec
* http://bit.ly/ao3pAB - Testing Quicksort with NaturalSpec

== Samples ==

You can write your spec mostly in natural language like in the following samples:

=== Sample 1 - Collections ===

[<Scenario>]
let ``When removing an element from a list it should not contain the element``() =
  Given [1;2;3;4;5]                 // "Arrange" test context
    |> When removing 3              // "Act"
    |> It shouldn't contain 3       // "Assert"
    |> It should contain 4          // another assertion
    |> It should have (Length 4)    // Assertion for length
    |> It shouldn't have Duplicates // Tests if the context contains duplicates
    |> Verify                       // Verify scenario

When running this sample the output is the following:

Scenario: When removing an element from a list it should not contain the element
  - Given [1; 2; 3; 4; 5]

    - When removing 3
      => It should not contain 3
      => It should contain 4
      => It should have length 4
      => It should not have Duplicates
  ==> OK
  ==> Time: 0.0239s

=== Sample 2 - Math ===

[<Scenario>]
let ``When calculating factorial of 5 it should equal 120``() =
  Given 5
    |> When calculating factorial
    |> It should equal 120
    |> Verify

The output of this sample is:

Scenario: When calculating factorial of 5 it should equal 120
  - Given 5
     - When calculating factorial 
      => It should equal 120
  ==> OK
  ==> Time: 0.0043s

=== Sample 3 - Mocking ===

[<Scenario>]
let ``When selling a car for 30000 it should equal the DreamCar mocked``() =
  As Bert
    |> Mock Bert.SellCar 30000 DreamCar
    |> When selling_a_car_for 30000
    |> It should equal DreamCar
    |> Verify

The output of this sample is:

Scenario: When selling a car for 30000 it should equal the DreamCar mocked
  - As Bert
     - With mocking
     - When selling a car for 30000
      => It should equal BMW (200 HP)
      => It should not equal Fiat (45 HP)
  ==> OK
  ==> Time: 0.0062s

Read more about "http://bit.ly/8ZtTTe - Mocking objects with NaturalSpec".


=== Sample 4 - Expected Failures ===

You can use the Attribute "Fail", "Fail_with" and "Fail_with_type" if you want a specific scenario to fail:

[<Scenario>]
[<Fails_with_type (typeof<DivideByZeroException>)>]
let ``When dividing by zero it should fail``() =
  Given 10
    |> When dividing_by 0
    |> Verify

[<Scenario>]
[<Fails_with "My error">]
let ``When raising exception``() =
  Given 0
    |> When raising "My error"
    |> Verify

The output would look like:

Scenario: When dividing by zero it should fail
  - Should fail with exception type System.DivideByZeroException

  - Given 10
     - When dividing by 0

Scenario: When raising exception
  - Should fail with "My error"
  - Given 0
     - When raising exception "My error"
     
=== Sample 5 - ScenarioTemplates ===

It is possible to use templates for scenarios:

// with Example attribute
[<Example(1, 1)>]  
[<Example(5, 120)>]
[<Example(10, 3628800)>]
let ``When calculating fac``(x,result) =
  Given x
    |> When calculating factorial
    |> It should equal result
      |> Verify

This code creates 3 scenarios and the output would look like:

Scenario: When calculating fac 
  - Given 1
     - When calculating factorial 
      => It should equal 1
  ==> OK
  ==> Time: 0.0292s

Scenario: When calculating fac 
  - Given 5
     - When calculating factorial 
      => It should equal 120
  ==> OK
  ==> Time: 0.0064s

Scenario: When calculating fac 
  - Given 10
     - When calculating factorial 
      => It should equal 3628800
  ==> OK
  ==> Time: 0.0058s
  
If you want more flexibility you can use the ScenarioSource attribute:

/// with a scenario source      
let MyTestCases =
  TestWith (tripleParam 12 3 4)
    |> And (tripleParam 12 4 3)
    |> And (tripleParam 12 6 2)
    |> And (tripleParam 0 0 0)    
        |> ShouldFailWith (typeof<System.DivideByZeroException>)
    |> And (tripleParam 1200 40 30)

[<ScenarioSource "MyTestCases">]
let ``When dividing`` a b result =
 Given a 
   |> When dividing_by b
   |> It should equal result
   |> Verify

Read more about "http://bit.ly/aqDrUj - Parameterized Scenarios with NaturalSpec".