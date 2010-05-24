module Euler.Problem52
  
open NaturalSpec

// Problem 52
// It can be seen that the number, 125874, and its double, 251748, 
// contain exactly the same digits, but in a different order.
//
// Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.

let normalize x = List.sort (digits x)

let HasSameDigits x y = 
    printMethod x
    normalize x = normalize y

let SmallestPositiveNumberWithProperty n =
    printMethod ""
    Seq.initInfinite id
      |> Seq.skip 1
      |> Seq.filter (fun x ->
            let x' = normalize x
            seq {2..n} 
              |> Seq.forall (fun f -> normalize (x * f) = x'))
      |> Seq.head

[<Scenario>]      
let ``Both numbers have same digits?``() =
    Given 125874
      |> When solving (HasSameDigits 251748)
      |> It should equal true
      |> Verify

[<Scenario>]      
let ``Find the smallest positive integer, x, such that 2x contain the same digits.``() =
    Given 2
      |> When solving SmallestPositiveNumberWithProperty
      |> It should equal 125874
      |> Verify

[<Scenario>]      
let ``Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.``() =
    Given 6
      |> When solving SmallestPositiveNumberWithProperty
      |> It should equal 142857
      |> Verify