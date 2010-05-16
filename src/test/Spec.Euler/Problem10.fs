module Problem10
  
open NaturalSpec

// Problem 10  
// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
//
// Find the sum of all the primes below two million.

let FindPrimes n =
    printMethod ""
    primes n
      |> Seq.toList

let FindSumOfPrimes n =
    printMethod ""
    primes n  
      |> Seq.map int64
      |> Seq.sum

[<Scenario>]      
let ``Find all the primes below 10``() =
    Given 10
      |> When solving FindPrimes
      |> It should equal [2; 3; 5; 7]
      |> Verify

[<Scenario>]      
let ``Find the sum of all the primes below 10``() =
    Given 10
      |> When solving FindSumOfPrimes
      |> It should equal 17L
      |> Verify

[<Scenario>]      
let ``Find the sum of all the primes below two million``() =
    Given 2000000
      |> When solving FindSumOfPrimes
      |> It should equal 142913828922L
      |> Verify