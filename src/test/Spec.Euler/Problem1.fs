module Problem1 
  
open NaturalSpec

/// Problem 1
/// If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. 
/// The sum of these multiples is 23.
///
/// Find the sum of all the multiples of 3 or 5 below 1000.
let SumOfMultiplesOf3And5 max = 
    printMethod ""
    [3..max-1] 
      |> List.filter (fun x -> x % 3 = 0 || x % 5 = 0)
      |> List.sum

[<Scenario>]     
let Problem1_WhenGettingSumOfMultiplesOf3And5_Below10 () =   
    Given 10
      |> When solving SumOfMultiplesOf3And5
      |> It should equal 23
      |> Verify

[<Scenario>]     
let Problem1_WhenGettingSumOfMultiplesOf3And5_Below1000 () =   
    Given 1000
      |> When solving SumOfMultiplesOf3And5
      |> It should equal 233168
      |> Verify