module Probability.Specs

open NaturalSpec
open Model

let combined_with x y=
    printMethod ""
    combine x y

[<Scenario>]     
let ``Certainly combined with Certainly should be Certainly `` () =   
    Given certainly
      |> When combined_with certainly
      |> It should equal certainly
      |> Verify