module Problem77
  
open NaturalSpec

// Problem 77
// It is possible to write ten as the sum of primes in exactly five different ways:
// 
// 7 + 3
// 5 + 5
// 5 + 3 + 2
// 3 + 3 + 2 + 2
// 2 + 2 + 2 + 2 + 2
// 
// What is the first value which can be written as the sum of primes in over five thousand different ways?

let p = primes 5000 |> Set.ofSeq

let FindSmallestValue count =
    printMethod ""

    allNumbersGreaterThan 4
      |> Seq.find (fun x -> Problem31.possibilities p x > count)

[<Scenario>]      
let ``What is the first value which can be written as the sum of primes in over 4 different ways?``() =
    Given 4
      |> When solving FindSmallestValue
      |> It should equal 10
      |> Verify

[<Scenario>]      
let ``What is the first value which can be written as the sum of primes in over five thousand different ways?``() =
    Given 5000
      |> When solving FindSmallestValue
      |> It should equal 71
      |> Verify