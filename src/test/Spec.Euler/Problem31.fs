module Problem31
  
open NaturalSpec

// Problem 31
// In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:
// 
// 1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
// 
// It is possible to make £2 in the following way:
// 
// 1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
// 
// How many different ways can £2 be made using any number of coins?

let rec possibilities numbers n=
    if n < (numbers |> Seq.head) then 0I else
    let m =
        numbers
          |> Seq.skipWhile (fun x -> x > n)
          |> Seq.map (fun x -> 
               let rest = numbers |> List.filter (fun y -> y <= x)
               possibilities rest (n-x))
          |> Seq.sum

    if numbers |> Seq.exists ((=) n) then m + 1I else m


let FindDifferentWays n =
    printMethod ""
    possibilities [1; 2; 5; 10; 20; 50; 100; 200] n
    
[<Scenario>]      
let ``How many different ways can 3cent be made using any number of coins?``() =
    Given 3
      |> When solving FindDifferentWays
      |> It should equal 2I
      |> Verify

[<Scenario>]      
let ``How many different ways can 6cent be made using any number of coins?``() =
    Given 6
      |> When solving FindDifferentWays
      |> It should equal 5I
      |> Verify


[<Scenario>]      
let ``How many different ways can 10cent be made using any number of coins?``() =
    Given 10
      |> When solving FindDifferentWays
      |> It should equal 11I
      |> Verify

[<Scenario>]      
let ``How many different ways can £2 be made using any number of coins?``() =
    Given 200
      |> When solving FindDifferentWays
      |> It should equal 73682I
      |> Verify