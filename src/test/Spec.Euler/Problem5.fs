module Problem5
  
open NaturalSpec

/// Problem 5
/// 2520 is the smallest number that can be divided by 
/// each of the numbers from 1 to 10 without any remainder.
///
/// What is the smallest number that is evenly divisible by all of the numbers from 1 to 20?
let FindLargestEvenlyDivisibleNumber max =
    printMethod ""   
    [1I..max] 
      |> List.reduce lcm 

[<Scenario>]      
let Problem5_WhenFindingLargestEvenlyDivisibleNumberUpTo10 () =     
    Given 10I
      |> When solving FindLargestEvenlyDivisibleNumber
      |> It should equal 2520I
      |> Verify  

[<Scenario>]      
let Problem5_WhenFindingLargestEvenlyDivisibleNumberUpTo20 () =     
    Given 20I
      |> When solving FindLargestEvenlyDivisibleNumber
      |> It should equal 232792560I
      |> Verify  