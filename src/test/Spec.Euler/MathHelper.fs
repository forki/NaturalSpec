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

let allNumbersGreaterThan x = Seq.unfold (fun s -> Some (s, s + 1)) x

let allPositiveNumbers = allNumbersGreaterThan 0

let bigint (x:int) = new System.Numerics.BigInteger(x)

let toHashTable xs =
    let h = new System.Collections.Generic.HashSet<_>()
    for x in xs do h.Add x |> ignore
    h

let rec transpose m =
    match m with
    | (_::_)::_ -> List.map List.head m:: transpose (List.map List.tail m)
    | _         -> []

let sqrt = System.Math.Sqrt
let floor (x:float) = System.Math.Floor(x)

let divisors n = 
  let max = float n |> sqrt |> floor |> int

  [for i in 1..max do
     if n % i = 0 then
       yield i
       let j = n / i
       if i <> j then yield j]

let pow a b = System.Numerics.BigInteger.Pow(a,b) 

let factorial n = seq { 1I..n } |> Seq.fold (*) 1I

let binomial n k = factorial n / (factorial k * factorial(n-k))

let rec sum_of_digits n =
    if n = 0I then 0I else
    n % 10I + sum_of_digits (n / 10I)