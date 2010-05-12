module Problem21
  
open NaturalSpec

// Problem 21
// Let d(n) be defined as the sum of proper divisors of n 
//  (numbers less than n which divide evenly into n).
// If d(a) = b and d(b) = a, where a <> b, then a and b are an amicable pair and 
// each of a and b are called amicable numbers.
//
// For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; 
// therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
//
// Evaluate the sum of all the amicable numbers under 10000.

let divisorSum n = (divisors n |> List.sum) - n

let isAmicable n = 
    let s = divisorSum n
    s <> n && n = divisorSum s 

let sumOfAmicableNumbers n =
    seq{ 1..n } 
      |> Seq.filter isAmicable
      |> Seq.sum

let Divisors n =
    printMethod ""
    divisors n
      |> List.sort

let DivisorSum n =
    printMethod ""
    divisorSum n

let IsAmicable n =
    printMethod ""
    isAmicable n

let SumOfAmicableNumbers n =
    printMethod ""
    sumOfAmicableNumbers n

[<Scenario>]      
let ``What are the divisors of 220?``() =
    Given 220
      |> When calculating Divisors
      |> It should equal [1; 2; 4; 5; 10; 11; 20; 22; 44; 55; 110; 220]
      |> Verify

[<Scenario>]      
let ``What are the divisors of 284?``() =
    Given 284
      |> When calculating Divisors
      |> It should equal [1; 2; 4; 71; 142; 284]
      |> Verify

[<Scenario>]      
let ``What is the sum of all divisors of 284?``() =
    Given 284
      |> When calculating DivisorSum
      |> It should equal 220
      |> Verify

[<Scenario>]      
let ``What is the sum of all divisors of 220?``() =
    Given 220
      |> When calculating DivisorSum
      |> It should equal 284
      |> Verify

[<Scenario>]      
let ``Is 220 amicable?``() =
    Given 220
      |> When calculating IsAmicable
      |> It should equal true
      |> Verify

[<Scenario>]      
let ``Is 284 amicable?``() =
    Given 284
      |> When calculating IsAmicable
      |> It should equal true
      |> Verify

[<Scenario>]      
let ``What is the sum of all the amicable numbers under 10000?``() =
    Given 10000
      |> When calculating SumOfAmicableNumbers
      |> It should equal 31626
      |> Verify