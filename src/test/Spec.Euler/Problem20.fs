module Problem20
  
open NaturalSpec

// Problem 20
// n! means n x (n x 1)  ...  3 x 2 x 1
// 
// Find the sum of the digits in the number 100!

[<Scenario>]      
let ``What is the sum of the digits of the number 2^15?``() =
    Given (factorial 3I)
      |> When solving SumOfDigits
      |> It should equal 6I
      |> Verify

[<Scenario>]      
let ``What is the sum of the digits of the number 2^1000?``() =
    Given (factorial 100I)
      |> When solving SumOfDigits
      |> It should equal 648I
      |> Verify