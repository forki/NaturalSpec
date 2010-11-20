module Tennis.Model

type Score =
| Love
| Fifteen

type Game = Score * Score

type Players =
| Player1
| Player2

let NewGame = Love,Love

let score game = function
| Player1 -> Fifteen,Love
