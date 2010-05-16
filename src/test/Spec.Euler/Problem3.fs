module Problem3
  
open NaturalSpec

// Problem 3  
// The prime factors of 13195 are 5, 7, 13 and 29.
//
// What is the largest prime factor of the number 600851475143 ?

let GetLargetsPrimeFactorOf max =
    printMethod ""
    let x =
      max 
        |> float
        |> System.Math.Sqrt
        |> int64
      
    primes x
      |> List.rev
      |> List.find (fun x -> max % x = 0L)

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