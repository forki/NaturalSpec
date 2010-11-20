module Tennis.Model

type Score =
| Love
| Fifteen
| Thirty

type Game = Score * Score

type Players =
| Player1
| Player2

let NewGame = Love,Love

let increaseScore = function
| Love -> Fifteen
| Fifteen -> Thirty

let score game = function
| Player1 -> fst game |> increaseScore,snd game
