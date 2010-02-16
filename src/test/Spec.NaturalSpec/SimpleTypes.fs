module NaturalSpec.SimpleTypes
open System

  
// Boolean
[<Scenario>]
let When_comparing_a_false_value() =
  Given false
    |> When doing nothing
    |> It should equal false
    |> It shouldn't equal true
    |> Verify
    
open NaturalSpec.Math          
        
// Integers
[<Scenario>]
let When_adding_0_to_3_it_should_not_change() =
  Given 3
    |> When adding 0
    |> It should equal 3
    |> It shouldn't equal 2
    |> Verify     

     
  
[<Scenario>]
[<FailsWithType (typeof<DivideByZeroException>)>]
let When_dividing_by_zero_it_should_fail() =
  Given 10
    |> When dividing_by 0
    |> Verify                              