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
| Deuce
| Advantage of Player
| Victory of Player

let inline (<=>) x y = OpenGame(x,y)

let NewGame = OpenGame(Love,Love)

let getScore = function 
| OpenGame(x,y) -> x,y
| Deuce -> 4,4
| Advantage player when player = Player.Player1 -> 5,4 
| Advantage player when player = Player.Player2 -> 4,5

let score game player =     
    let oldScore = getScore game
    let newScore =
        match player with
        | Player.Player1 -> fst oldScore + 1,snd oldScore
        | Player.Player2 -> fst oldScore,snd oldScore + 1
    match newScore with
    | x,y when x > Fourty && y > Fourty && x = y -> Deuce
    | x,y when x > Fourty && x-y = 1 -> Advantage Player.Player1
    | x,y when y > Fourty && y-x = 1 -> Advantage Player.Player2
    | x,y when x > Fourty && x > y-> Victory Player.Player1
    | x,y when y > Fourty && y > x-> Victory Player.Player2
    | _ -> OpenGame newScore