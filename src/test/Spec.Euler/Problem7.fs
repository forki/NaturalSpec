module Problem7
  
open NaturalSpec

// Problem 7
// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
//
// What is the 10001st prime number?

let FindPrimeNumber n =
    printMethod ""   
    primes 140000
      |> Seq.skip (n-1)
      |> Seq.head

[<Scenario>]      
let ``What is the 6th prime number?``() =
    Given 6
      |> When solving FindPrimeNumber
      |> It should equal 13
      |> Verify  

[<Scenario>]      
let ``What is the 10001st prime number?``() =
    Given 10001
      |> When solving FindPrimeNumber
      |> It should equal 104743
      |> Verify  