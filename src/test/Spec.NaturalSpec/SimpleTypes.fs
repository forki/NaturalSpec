module NaturalSpec.SimpleTypes
  
// Boolean
[<Scenario>]
let ``Comparing a false value``() =
  Given false
    |> When doing nothing
    |> It should equal false
    |> It shouldn't equal true
    |> Verify       
        
// Integers
[<Scenario>]
let ``3 + 0 should give 3``() =
  Given 3
    |> When adding 0
    |> It should equal 3
    |> It shouldn't equal 2
    |> Verify  
  
[<Scenario>]
[<FailsWithType (typeof<System.DivideByZeroException>)>]
let ``Dividing by zero should fail``() =
  Given 10
    |> When dividing_by 0
    |> Verify                              