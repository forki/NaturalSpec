module Tennis.Specs

open NaturalSpec
open Model

let Player1 = 1
let Player2 = 2

let point_goes_to player game =
    let player = Player.FromInt player
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
      |> When point_goes_to Player1
      |> It should equal (Fifteen <=> Love)
      |> Verify

[<Scenario>]     
let ``When player one scores twice the score should be Thirty to Love`` () =   
    Given NewGame
      |> When point_goes_to Player1
      |> When point_goes_to Player1
      |> It should equal (Thirty <=> Love)
      |> Verify

[<Scenario>]     
let ``When player two scores 3 times the score should be Love to Fourty`` () =   
    Given NewGame
      |> When point_goes_to Player2
      |> When point_goes_to Player2
      |> When point_goes_to Player2
      |> It should equal (Love <=> Fourty)
      |> Verify

[<Example(1)>]  
[<Example(2)>]  
let ``If a player scores 4 times and the other did not score he has won`` winner =
    Given NewGame
      |> When point_goes_to winner
      |> When point_goes_to winner
      |> When point_goes_to winner
      |> When point_goes_to winner
      |> It should equal (Victory <| Player.FromInt winner)
      |> Verify

[<Example(1)>]  
[<Example(2)>]  
let ``When a player scores after a deuce he should have the advantage`` player =
    Given Deuce
      |> When point_goes_to player
      |> It should equal (Advantage <| Player.FromInt player)
      |> Verify

[<Scenario>] 
let ``If both players score 4 times the game should be deuce``() =
    Given (Fourty <=> Fourty)
      |> When point_goes_to Player1
      |> When point_goes_to Player2
      |> It should equal Deuce
      |> Verify

[<Example(1)>]  
[<Example(2)>]  
let ``When a player scores after he got advantage he has won`` player =
    Given (Advantage <| Player.FromInt player)
      |> When point_goes_to player
      |> It should equal (Victory <| Player.FromInt player)
      |> Verify

[<Example(1,2)>]  
[<Example(2,1)>]  
let ``When a player looses his advantage they should be back to deuce`` player opponent =
    Given (Advantage <| Player.FromInt player)
      |> When point_goes_to opponent
      |> It should equal Deuce
      |> Verify

[<Example(1,1)>]  
[<Example(1,2)>]  
[<Example(2,1)>]  
[<Example(2,2)>]  
let ``A won game is not changable`` player scorer =
    Given (Victory <| Player.FromInt player)
      |> When point_goes_to scorer
      |> It should equal (Victory <| Player.FromInt player)
      |> Verify
