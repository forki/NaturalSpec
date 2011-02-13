module Euler.Problem23
  
open NaturalSpec

// Problem 23
// A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. 
// For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28,
//  which means that 28 is a perfect number.
//
// A number n is called deficient if the sum of its proper divisors is less than n 
// and it is called abundant if this sum exceeds n.
//
// As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, 
// the smallest number that can be written as the sum of two abundant numbers is 24. 
// By mathematical analysis, it can be shown that all integers greater than 28123 
// can be written as the sum of two abundant numbers. 
//
// However, this upper limit cannot be reduced any further by analysis even though it is known 
// that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.
//
// Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.

let isAbundant number =
    divisors number
      |> Seq.sum
      |> fun x -> x > 2 * number

let IsAbundant number =
    printMethod ""
    isAbundant number

let abundantNumbers n = 
    seq {1..n}
      |> Seq.filter isAbundant
      |> Set.ofSeq

let isComposed abundantNumbers number =
    abundantNumbers
      |> Set.exists (fun a -> Set.contains (number - a) abundantNumbers)

let IsComposed number =
    printMethod ""
    isComposed (abundantNumbers number) number

let SumOfUncomposableNumbers number =
    printMethod ""
    let a = abundantNumbers number
    seq {1..number}
      |> Seq.filter (not << isComposed a)
      |> Seq.sum

      
[<Example(11,false)>]
[<Example(12,true)>]
[<Example(28,false)>]
let ``What is n abundant?`` (n,abundant) =
    Given n
      |> When calculating IsAbundant
      |> It should equal abundant
      |> Verify

[<Example(11,false)>]
[<Example(24,true)>]
[<Example(28124,true)>]
let ``What is n composed?`` (n,composed) =
    Given n
      |> When calculating IsComposed
      |> It should equal composed
      |> Verify

[<Scenario>]
let ``Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.`` () =
    Given 28124
      |> When calculating SumOfUncomposableNumbers
      |> It should equal 4179871
      |> Verify