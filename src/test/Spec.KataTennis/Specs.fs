module Tennis.Specs

open NaturalSpec
open Model

let point_goes_to player game =
    printMethod player
    score game player


[<Scenario>]     
let ``A newly started game should start with score Love to Love`` () =   
    Given NewGame
      |> It should equal (Love,Love)
      |> Verify

[<Scenario>]     
let ``When Player1 scores once the score should be Fifteen to Love`` () =   
    Given NewGame
      |> When point_goes_to Player1
      |> It should equal (Fifteen,Love)
      |> Verify

[<Scenario>]     
let ``When player one scores twice the score should be Thirty to Love`` () =   
    Given NewGame
      |> When point_goes_to Player1
      |> When point_goes_to Player1
      |> It should equal (Thirty,Love)
      |> Verify