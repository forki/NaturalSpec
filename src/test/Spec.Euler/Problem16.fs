module Euler.Problem16
  
open NaturalSpec

// Problem 16
// 2^15 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.
//
// What is the sum of the digits of the number 2^1000?

[<Scenario>]      
let ``What is the sum of the digits of the number 2^15?``() =
    Given (pow 2I 15)
      |> When solving SumOfDigits
      |> It should equal 26I
      |> Verify

[<Scenario>]      
let ``What is the sum of the digits of the number 2^1000?``() =
    Given (pow 2I 1000)
      |> When solving SumOfDigits
      |> It should equal 1366I
      |> Verify