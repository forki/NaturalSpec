module Euler.Problem53
  
open NaturalSpec

// Problem 53
//
// There are exactly ten ways of selecting three from five, 12345:
//
//   123, 124, 125, 134, 135, 145, 234, 235, 245, and 345
//
// In combinatorics, we use the notation, C(5,3) = 10.
//
// In general, C(n,r) = n! / (r!(n−r)!)
//	,where r <= n, n! = n×(n−1)×...×3×2×1, and 0! = 1.
//
// It is not until n = 23, that a value exceeds one-million: C(23,10) = 1144066.
//
// How many, not necessarily distinct, values of  C(n,r), for 1 <= n <= 100, are greater than one-million?

let Combinatorics _ =
    printMethod ""
    seq { for n in 2I..100I do
            for k in 2I..n -> n,k }
      |> Seq.map binomial
      |> Seq.filter (fun x -> x > 1000000I)
      |> Seq.length

[<Scenario>]      
let ``How many, not necessarily distinct, values of C are greater than one-million?``() =
    Given ()
      |> When solving Combinatorics
      |> It should equal 4075
      |> Verify