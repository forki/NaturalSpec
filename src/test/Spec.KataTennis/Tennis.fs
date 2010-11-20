module Tennis.Model

let Love = 0
let Fifteen = 1
let Thirty = 2
let Fourty = 3

type Player =
| Player1 = 0
| Player2 = 1

type Game = 
| OpenGame of int * int
| Victory of Player

let inline (<=>) x y = OpenGame(x,y)

let NewGame = OpenGame(Love,Love)

let getScore = function | OpenGame(x,y) -> x,y

let score game player = 
    let oldScore = getScore game
    let newScore =
        match player with
        | Player.Player1 -> fst oldScore + 1,snd oldScore
        | Player.Player2 -> fst oldScore,snd oldScore + 1
    match newScore with
    | _,x when x > Fourty -> Victory Player.Player2
    | x,_ when x > Fourty -> Victory Player.Player1
    | _ -> OpenGame newScore