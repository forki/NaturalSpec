module Euler.Problem30
  
open NaturalSpec

// Problem 30
// Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:
//
//    1634 = 1^(4) + 6^(4) + 3^(4) + 4^(4)
//    8208 = 8^(4) + 2^(4) + 0^(4) + 8^(4)
//    9474 = 9^(4) + 4^(4) + 7^(4) + 4^(4)
//
// As 1 = 1^(4) is not a sum it is not included.
//
// The sum of these numbers is 1634 + 8208 + 9474 = 19316.
//
// Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.

let test digit number =
    digits number
        |> Seq.map (fun x -> System.Math.Pow(float x,float digit))
        |> Seq.sum
        |> fun x -> int x = number
        
let Digits n =
    printMethod ""
    digits n

let DigitsToNumber n =
    printMethod ""
    digitsToNumber n

let IsComposableBy digit n =
    printMethod digit 
    test digit n

let composableNumbers digit n =
    seq { 2..n}
      |> Seq.filter (test digit)
      |> Seq.sum

let SumComposableNumbersBy digit n =
    printMethod digit 
    composableNumbers digit n

[<Scenario>]      
let ``What are the digits in the number 1634?``() =
    Given 1634
      |> When calculating Digits
      |> It should equal [1;6;3;4]
      |> Verify

[<Scenario>]      
let ``What is number for [1;6;3;4]?``() =
    Given [1;6;3;4]
      |> When calculating DigitsToNumber
      |> It should equal 1634
      |> Verify

[<Example(1634,true)>]
[<Example(8208,true)>]
[<Example(9474,true)>]
let ``Is the number by 4 composable?``(n,result) =
    Given n
      |> When calculating (IsComposableBy 4)
      |> It should equal result
      |> Verify

[<Scenario>]
let ``Find the sum of all the numbers that can be written as the sum of fourth powers of their digits.``() =
    Given 10000
      |> When calculating (SumComposableNumbersBy 4)
      |> It should equal 19316
      |> Verify

[<Scenario>]
let ``Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.``() =
    Given 1000000
      |> When calculating (SumComposableNumbersBy 5)
      |> It should equal 443839
      |> Verify