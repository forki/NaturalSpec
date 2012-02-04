module Sort.Specs

open NaturalSpec

open Model

[<Scenario>]     
let ``NaturalSpec should work`` () =   
    Given 1
      |> It should equal 1
      |> Verify
