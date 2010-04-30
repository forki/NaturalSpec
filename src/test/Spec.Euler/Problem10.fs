module Problem10
  
open NaturalSpec

// Problem 10  
// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
//
// Find the sum of all the primes below two million.

let FindSumOfPrimes n =
    printMethod ""
    primes n  
      |> List.sum 

[<Scenario>]      
let ``Find the sum of all the primes below 10``() =
    Given 10I
      |> When solving FindSumOfPrimes
      |> It should equal 17I
      |> Verify


[<Scenario>]      
let ``Find the sum of all the primes below two million``() =
    Given 2000000I
      |> When solving FindSumOfPrimes
      |> It should equal 142913828922I
      |> Verify