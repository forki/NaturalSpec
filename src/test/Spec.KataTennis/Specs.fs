module Tennis.Specs

open NaturalSpec
open Model

let point_goes_to player game =
    printMethod player
    score game player

[<Scenario>]     
let ``A newly started game should start with score Love to Love`` () =   
    Given NewGame
      |> It should equal (Love <=> Love)
      |> Verify

[<Scenario>]     
let ``When Player1 scores once the score should be Fifteen to Love`` () =   
    Given NewGame
      |> When point_goes_to Player.Player1
      |> It should equal (Fifteen <=> Love)
      |> Verify

[<Scenario>]     
let ``When player one scores twice the score should be Thirty to Love`` () =   
    Given NewGame
      |> When point_goes_to Player.Player1
      |> When point_goes_to Player.Player1
      |> It should equal (Thirty <=> Love)
      |> Verify

[<Scenario>]     
let ``When player two scores 3 times the score should be Love to Fourty`` () =   
    Given NewGame
      |> When point_goes_to Player.Player2
      |> When point_goes_to Player.Player2
      |> When point_goes_to Player.Player2
      |> It should equal (Love <=> Fourty)
      |> Verify

[<ScenarioTemplate(Player.Player1)>]  
[<ScenarioTemplate(Player.Player2)>]  
let ``If a player scores 4 times and the other did not score he has won`` winner =
    Given NewGame
      |> When point_goes_to winner
      |> When point_goes_to winner
      |> When point_goes_to winner
      |> When point_goes_to winner
      |> It should equal (Victory winner)
      |> Verify

[<ScenarioTemplate(Player.Player1)>]  
[<ScenarioTemplate(Player.Player2)>]  
let ``When a player scores after a deuce he should have the advantage`` player =
    Given Deuce
      |> When point_goes_to player
      |> It should equal (Advantage player)
      |> Verify

[<Scenario>] 
let ``If both players score 4 times the game should be deuce``() =
    Given NewGame
      |> When point_goes_to Player.Player1
      |> When point_goes_to Player.Player2
      |> When point_goes_to Player.Player1
      |> When point_goes_to Player.Player2
      |> When point_goes_to Player.Player1
      |> When point_goes_to Player.Player2
      |> When point_goes_to Player.Player1
      |> When point_goes_to Player.Player2
      |> It should equal Deuce
      |> Verify

[<ScenarioTemplate(Player.Player1)>]  
[<ScenarioTemplate(Player.Player2)>]  
let ``When a player scores after he got advantage he has won`` player =
    Given (Advantage player)
      |> When point_goes_to player
      |> It should equal (Victory player)
      |> Verify

[<ScenarioTemplate(Player.Player1,Player.Player2)>]  
[<ScenarioTemplate(Player.Player2,Player.Player1)>]  
let ``When a player looses his advantage they should be back to deuce`` player opponent =
    Given (Advantage player)
      |> When point_goes_to opponent
      |> It should equal Deuce
      |> Verify

[<ScenarioTemplate(Player.Player1,Player.Player1)>]  
[<ScenarioTemplate(Player.Player1,Player.Player2)>]  
[<ScenarioTemplate(Player.Player2,Player.Player1)>]  
[<ScenarioTemplate(Player.Player2,Player.Player2)>]  
let ``A won game is not changable`` player scorer =
    Given (Victory player)
      |> When point_goes_to scorer
      |> It should equal (Victory player)
      |> Verify
