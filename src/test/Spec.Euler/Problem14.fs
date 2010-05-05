module Problem14
  
open NaturalSpec

// Problem 14 
// The following iterative sequence is defined for the set of positive integers:
// 
// n -> n/2 (n is even)
// n -> 3n + 1 (n is odd)
// 
// Using the rule above and starting with 13, we generate the following sequence:
// 13 40 20 10 5 16 8 4 2 1
// 
// It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms.
//  Although it has not been proved yet (Collatz Problem), 
// it is thought that all starting numbers finish at 1.
// 
// Which starting number, under one million, produces the longest chain?
// 
// NOTE: Once the chain starts the terms are allowed to go above one million.

let dict = new System.Collections.Generic.Dictionary<_,_>() 
let addToDict n t =
    if n < 100000I then
        dict.[n] <- t
    t
    
let rec chainLength n = 
    match n with
    | _ when n = 1I              -> 1I
    | _ when dict.ContainsKey n  -> dict.[n]
    | _ when n % 2I = 0I         -> 1I + chainLength (n / 2I)      |> addToDict n 
    | _                          -> 1I + chainLength (3I * n + 1I) |> addToDict n 

let FindChainLength n =
    printMethod ""
    chainLength n

let FindLongestChainStart n =
    printMethod ""
    seq { 1I..n }
      |> Seq.maxBy chainLength

[<Scenario>]      
let ``Find chain length for 13``() =
    Given 13I
      |> When solving FindChainLength
      |> It should equal 10I
      |> Verify

[<Scenario>]      
let ``Find the starting number, under one million, which produces the longest chain``() =
    Given 1000000I
      |> When solving FindLongestChainStart
      |> It should equal 837799I
      |> Verify