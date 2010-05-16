module Problem10
  
open NaturalSpec

// Problem 10  
// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
//
// Find the sum of all the primes below two million.

let FindPrimes n =
    printMethod ""
    primes n 

let FindSumOfPrimes n =
    printMethod ""
    primes n  
      |> List.sum 

[<Scenario>]      
let ``Find all the primes below 10``() =
    Given 10L
      |> When solving FindPrimes
      |> It should equal [2L; 3L; 5L; 7L]
      |> Verify

[<Scenario>]      
let ``Find the sum of all the primes below 10``() =
    Given 10L
      |> When solving FindSumOfPrimes
      |> It should equal 17L
      |> Verify

[<Scenario>]      
let ``Find the sum of all the primes below two million``() =
    Given 2000000L
      |> When solving FindSumOfPrimes
      |> It should equal 142913828922L
      |> Verify