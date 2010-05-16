module Euler.Problem14
  
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
let inline addToDict n t =
    dict.[n] <- t
    t
    
let rec chainLength n = 
    match n with
    | _ when n = 1L              -> 1L
    | _ when dict.ContainsKey n  -> dict.[n]
    | _ when n % 2L = 0L         -> 1L + chainLength (n / 2L)      |> addToDict n 
    | _                          -> 1L + chainLength (3L * n + 1L) |> addToDict n 

let FindChainLength n =
    printMethod ""
    chainLength n

let FindLongestChainStart n =
    printMethod ""
    seq { 1L..n }
      |> Seq.maxBy chainLength

[<Scenario>]      
let ``Find chain length for 13``() =
    Given 13L
      |> When solving FindChainLength
      |> It should equal 10L
      |> Verify

[<Scenario>]      
let ``Find the starting number, under one million, which produces the longest chain``() =
    Given 1000000L
      |> When solving FindLongestChainStart
      |> It should equal 837799L
      |> Verify