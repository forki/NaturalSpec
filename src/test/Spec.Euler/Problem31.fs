module Euler.Problem31
  
open NaturalSpec

// Problem 31
// In England the currency is made up of pound, £, and pence, p, 
// and there are eight coins in general circulation:
// 
// 1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
// 
// It is possible to make £2 in the following way:
// 
// 1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
// 
// How many different ways can £2 be made using any number of coins?

let rec possibilities numbers n =
    if n < Set.minElement numbers then 0 else
    numbers
      |> Set.filter (fun x -> x <= n)
      |> Seq.map (fun x -> 
            let rest = numbers |> Set.filter (fun y -> y <= x)
            possibilities rest (n-x))
      |> Seq.sum
      |> fun m -> if Set.contains n numbers then m + 1 else m


let FindDifferentWays n =
    printMethod ""
    possibilities 
      (Set.ofList [1; 2; 5; 10; 20; 50; 100; 200])
      n
    
[<Scenario>]      
let ``How many different ways can 3cent be made using any number of coins?``() =
    Given 3
      |> When solving FindDifferentWays
      |> It should equal 2
      |> Verify

[<Scenario>]      
let ``How many different ways can 6cent be made using any number of coins?``() =
    Given 6
      |> When solving FindDifferentWays
      |> It should equal 5
      |> Verify


[<Scenario>]      
let ``How many different ways can 10cent be made using any number of coins?``() =
    Given 10
      |> When solving FindDifferentWays
      |> It should equal 11
      |> Verify

[<Scenario>]      
let ``How many different ways can £2 be made using any number of coins?``() =
    Given 200
      |> When solving FindDifferentWays
      |> It should equal 73682
      |> Verify