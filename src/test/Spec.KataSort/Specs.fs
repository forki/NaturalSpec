module Sort.Specs

open NaturalSpec

open Model

let sorting list =
    printMethod ""
    sort list

[<Scenario>]     
let ``When sorting an empty list it should return an empty list`` () =   
    Given []
      |> When sorting
      |> It should equal []
      |> Verify
      
[<Scenario>]     
let ``When sorting a singleton list it should return the same list`` () =   
    Given [1]
      |> When sorting
      |> It should equal [1]
      |> Verify