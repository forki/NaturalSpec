module Tennis.Specs

open NaturalSpec
open Model

[<Scenario>]     
let ``A newly started game should start with score Love to Love`` () =   
    Given NewGame
      |> It should equal (Love,Love)
      |> Verify