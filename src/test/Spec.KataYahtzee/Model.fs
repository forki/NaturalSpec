module Yahtzee.Model

type Roll = int * int * int * int * int

type Category =
| Ones
| Twos
| Threes
| Fours
| Fives
| Sixes

let toList (roll:Roll) =
    let a,b,c,d,e = roll
    [a;b;c;d;e]

let sumNumber number =
    Seq.filter ((=) number)
      >> Seq.sum


let calcValue category roll =
    let list = toList roll
    match category with
    | Ones   -> sumNumber 1 list
    | Twos   -> sumNumber 2 list
    | Threes -> sumNumber 3 list
    | Fours  -> sumNumber 4 list
    | Fives  -> sumNumber 5 list
    | Sixes  -> sumNumber 6 list