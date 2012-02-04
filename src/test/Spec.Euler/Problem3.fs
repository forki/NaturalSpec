module Euler.Problem3
  
open NaturalSpec

// Problem 3  
// The prime factors of 13195 are 5, 7, 13 and 29.
//
// What is the largest prime factor of the number 600851475143 ?


let getting_primeFactors number =
    printMethod ""
    primeFactors number

[<Scenario>]
let ``PrimeFactors of 1`` () =
    Given 1L
      |> When getting_primeFactors
      |> It should equal []
      |> Verify

[<Scenario>]
let ``PrimeFactors of 2`` () =
    Given 2L
      |> When getting_primeFactors
      |> It should equal [2L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 3`` () =
    Given 3L
      |> When getting_primeFactors
      |> It should equal [3L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 4`` () =
    Given 4L
      |> When getting_primeFactors
      |> It should equal [2L;2L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 5`` () =
    Given 5L
      |> When getting_primeFactors
      |> It should equal [5L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 6`` () =
    Given 6L
      |> When getting_primeFactors
      |> It should equal [2L;3L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 7`` () =
    Given 7L
      |> When getting_primeFactors
      |> It should equal [7L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 8`` () =
    Given 8L
      |> When getting_primeFactors
      |> It should equal [2L;2L;2L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 12`` () =
    Given 12L
      |> When getting_primeFactors
      |> It should equal [2L;2L;3L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 15`` () =
    Given 15L
      |> When getting_primeFactors
      |> It should equal [3L;5L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of 25`` () =
    Given 25L
      |> When getting_primeFactors
      |> It should equal [5L;5L]
      |> Verify

[<Scenario>]
let ``PrimeFactors of LargeNumber`` () =
    Given (5L*5L*11L*13L*97L)
      |> When getting_primeFactors
      |> It should equal [5L;5L;11L;13L;97L]
      |> Verify

let GetLargetsPrimeFactorOf number =
    printMethod ""
    primeFactors number
    |> List.max

[<Scenario>]      
let ``What is the largest prime factor of the number 13195?`` () =
    Given 13195L
      |> When solving GetLargetsPrimeFactorOf
      |> It should equal 29L
      |> Verify  

[<Scenario>]      
let ``What is the largest prime factor of the number 600851475143 ?`` () =
    Given 600851475143L
      |> When solving GetLargetsPrimeFactorOf
      |> It should equal 6857L
      |> Verify