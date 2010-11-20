module Tennis.Model

type Score =
| Love

type Game = Score * Score

let NewGame = Love,Love