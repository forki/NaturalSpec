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
      