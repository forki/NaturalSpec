[<AutoOpen>]
module MathHelper

/// Gets a infinite sequence of fibonacci numbers
let fibs = 
    Seq.unfold (fun (a,b) -> 
      let sum = a + b
      Some(sum, (b, sum))) 
      (0,1)

/// Returns a list with primes  
let primes (n:bigint) =    
    let max = System.Math.Sqrt (float n)
    let rec filterPrimes = function
        | x::rest -> 
            if float x <= max then              
              let r = 
                rest
                  |> List.filter (fun num -> num % x <> 0I)
                  |> filterPrimes
              x :: r
            else
              x::rest
        | [] -> []

    match n with
    | x when x = 1I -> [2I]
    | _ -> filterPrimes <| 2I :: [3I..2I..n]


/// Returns the greates common devisor
let rec gcd a b =
   if b = 0I then
     a 
   else
     gcd b (a%b)

/// Returns the least common multiple
let lcm a b = a * b / (gcd a b)