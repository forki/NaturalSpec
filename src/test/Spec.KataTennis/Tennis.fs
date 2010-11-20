module Tennis.Model

type Score =
| Love
| Fifteen
| Thirty
| Fourty

type Game = Score * Score

type Players =
| Player1
| Player2

let NewGame = Love,Love

let increaseScore = function
| Love -> Fifteen
| Fifteen -> Thirty
| Thirty -> Fourty

let score game = function
| Player1 -> fst game |> increaseScore,snd game
| Player2 -> fst game,snd game |> increaseScore
