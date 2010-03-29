module Problem4
  
open NaturalSpec

/// Problem 4  
/// A palindromic number reads the same both ways. 
/// The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 * 99.
///
/// Find the largest palindrome made from the product of two 3-digit numbers.
let FindLargestPalindrome max =
    printMethod ""   
    seq {
       for i in 1..max do
         for j in 1..max do
           let x = i*j
           if isPalindrome <| x.ToString() then
             yield x } 
      |> Seq.max

[<Scenario>]      
let Problem4_WhenFindingLargetsPalindromeMadeFromTheProductOf2DigitNumer () =     
    Given 99
      |> When solving FindLargestPalindrome
      |> It should equal 9009
      |> Verify  

[<Scenario>]      
let Problem4_WhenFindingLargetsPalindromeMadeFromTheProductOf3DigitNumer () =     
    Given 999
      |> When solving FindLargestPalindrome
      |> It should equal 906609
      |> Verify  