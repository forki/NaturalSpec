module Yahtzee.Model

type Roll = int * int * int * int * int

type Category =
| Ones
| Twos
| Threes
| Fours
| Fives
| Sixes
| Pair
| TwoPair

let toList (roll:Roll) =
    let a,b,c,d,e = roll
    [a;b;c;d;e]

let sumNumber number =
    Seq.filter ((=) number)
      >> Seq.sum

let sumAsPair list number =
    let numberCount = list |> Seq.filter ((=) number) |> Seq.length
    if numberCount >= 2 then 2 * number else 0
    
let allNumbers = [1..6]
let allPairs =
    [for i in allNumbers do
       for j in allNumbers -> i,j]
      
let takeBest = Seq.max

let calcValue category roll =
    let list = toList roll
    match category with
    | Ones   -> sumNumber 1 list
    | Twos   -> sumNumber 2 list
    | Threes -> sumNumber 3 list
    | Fours  -> sumNumber 4 list
    | Fives  -> sumNumber 5 list
    | Sixes  -> sumNumber 6 list
    | Pair   -> 
        allNumbers
          |> Seq.map (sumAsPair list)
          |> takeBest
    | TwoPair   -> 
        allPairs
          |> Seq.filter (fun (a,b) -> a <> b)
          |> Seq.map (fun (a,b) -> 
                let a' = sumAsPair list a
                let b' = sumAsPair list b
                if a' = 0 || b' = 0 then 0 else a' + b')
          |> takeBest