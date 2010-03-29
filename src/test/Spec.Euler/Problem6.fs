module Problem6
  
open NaturalSpec

/// Problem 6
/// The sum of the squares of the first ten natural numbers is,
///     1² + 2² + ... + 10² = 385
/// The square of the sum of the first ten natural numbers is,
///     (1 + 2 + ... + 10)² = 552 = 3025
///
/// Hence the difference between the sum of the squares of the first ten natural numbers and 
/// the square of the sum is 3025 - 385 = 2640.
///
/// Find the difference between the sum of the squares of the first one hundred natural numbers 
/// and the square of the sum.
let FindDifferenceBetweenSumOfSquaresAndSquareOfSum max =
    let sq (x:bigint) = x*x

    printMethod ""   
    let sumOfSq = 
        [1I..max]
          |> List.map sq
          |> List.sum

    let sqOfSum = 
        [1I..max]
          |> List.sum
          |> sq
    sumOfSq - sqOfSum |> abs

[<Scenario>]      
let ``Find the difference between the sum of the squares of the first 10 natural numbers and the square of the sum.``() =     
    Given 10I
      |> When solving FindDifferenceBetweenSumOfSquaresAndSquareOfSum
      |> It should equal 2640I
      |> Verify  

[<Scenario>]      
let ``Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.``() =     
    Given 100I
      |> When solving FindDifferenceBetweenSumOfSquaresAndSquareOfSum
      |> It should equal 25164150I
      |> Verify  