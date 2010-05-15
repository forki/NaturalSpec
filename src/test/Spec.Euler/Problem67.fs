module Problem67
  
open NaturalSpec

// Problem 67
// By starting at the top of the triangle below and moving to adjacent numbers on the row below, 
// the maximum total from top to bottom is 23.
//
//    3
//   7 4
//  2 4 6
// 8 5 9 3
//
// That is, 3 + 7 + 4 + 9 = 23.
//
// Find the maximum total from top to bottom in triangle.txt, 
// a 15K text file containing a triangle with one-hundred rows.
//
// NOTE: This is a much more difficult version of Problem 18. 
// It is not possible to try every route to solve this problem, as there are 2^(99) altogether! 
// If you could check one trillion (10^(12)) routes every second it would take over twenty billion years
// to check them all. There is an efficient algorithm to solve it. ;o)

let triangle =
   System.IO.File.ReadAllLines "triangle.txt"
     |> Array.map (fun line -> line.Split [| ' ' |] |> Array.map (System.Int32.Parse))


[<Scenario>]      
let ``Find the maximum total from top to bottom of the triangle``() =
    Given triangle
      |> When solving Problem18.Triangle
      |> It should equal 7273
      |> Verify