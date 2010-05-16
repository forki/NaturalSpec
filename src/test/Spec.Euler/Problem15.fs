module Problem15
  
open NaturalSpec

// Problem 15
// Starting in the top left corner of a 2 x 2 grid, there are 6 routes (without backtracking) 
// to the bottom right corner.
//
// How many routes are there through a 20 x 20 grid?

let CountRoutes n =
    printMethod ""
    binomial (2I*n,n)

[<Scenario>]      
let ``How many routes are there through a 2 x 2 grid?``() =
    Given 2I
      |> When solving CountRoutes
      |> It should equal 6I
      |> Verify

[<Scenario>]      
let ``How many routes are there through a 3 x 3 grid?``() =
    Given 3I
      |> When solving CountRoutes
      |> It should equal 20I
      |> Verify

[<Scenario>]      
let ``How many routes are there through a 20 x 20 grid?``() =
    Given 20I
      |> When solving CountRoutes
      |> It should equal 137846528820I
      |> Verify