module Yahtzee.Specs

open NaturalSpec
open Model

let placed_on category =
    printMethod category
    calcValue category 

// Ones, Twos, Threes, Fours, Fives, Sixes: 
// The player scores the sum of the dice that reads one, two, three, four, five or six, respectively. 
// For example, 1, 1, 2, 4, 4 placed on "fours" gives 8 points.

[<Scenario>]     
let ``Given 1, 1, 2, 4, 4 placed on "fours" gives 8 points.`` () =   
    Given (1, 1, 2, 4, 4)
      |> When (placed_on Fours)
      |> It should equal 8
      |> Verify

[<Scenario>]     
let ``Given 1, 1, 6, 4, 6 placed on "sixes" gives 12 points.`` () =   
    Given (1, 1, 6, 4, 6)
      |> When (placed_on Sixes)
      |> It should equal 12
      |> Verify

// Pair: The player scores the sum of the two highest matching dice. 
// For example, 3, 3, 3, 4, 4 placed on "pair" gives 8.

[<Scenario>]     
let ``Given 3, 3, 3, 4, 4 placed on "pair" gives 8.`` () =   
    Given (3, 3, 3, 4, 4)
      |> When (placed_on Pair)
      |> It should equal 8
      |> Verify

[<Scenario>]     
let ``Given 5, 3, 5, 4, 4 placed on "pair" gives 10.`` () =   
    Given (5, 3, 5, 4, 4)
      |> When (placed_on Pair)
      |> It should equal 10
      |> Verify

[<Scenario>]     
let ``Given 5, 3, 2, 4, 1 placed on "pair" gives 0.`` () =   
    Given (5, 3, 2, 4, 1)
      |> When (placed_on Pair)
      |> It should equal 0
      |> Verify

// Two pairs: If there are two pairs of dice with the same number, 
// the player scores the sum of these dice. If not, the player scores 0. 
// For example, 1, 1, 2, 3, 3 placed on "two pairs" gives 8.

[<Scenario>]     
let ``Given 1, 1, 2, 3, 3 placed on "two pair" gives 8.`` () =   
    Given (1, 1, 2, 3, 3)
      |> When (placed_on TwoPair)
      |> It should equal 8
      |> Verify

[<Scenario>]     
let ``Given 1, 6, 6, 3, 3 placed on "two pair" gives 18.`` () =   
    Given (1, 6, 6, 3, 3)
      |> When (placed_on TwoPair)
      |> It should equal 18
      |> Verify

[<Scenario>]     
let ``Given 1, 1, 2, 4, 3 placed on "two pair" gives 0.`` () =   
    Given (1, 1, 2, 4, 3)
      |> When (placed_on TwoPair)
      |> It should equal 0
      |> Verify

// Three of a kind: If there are three dice with the same number, 
// the player scores the sum of these dice. Otherwise, the player scores 0. 
// For example, 3, 3, 3, 4, 5 places on "three of a kind" gives 9.

[<Scenario>]     
let ``Given 3, 3, 3, 4, 5 placed on "three of a kind" gives 9.`` () =   
    Given (3, 3, 3, 4, 5)
      |> When (placed_on ThreeOfAKind)
      |> It should equal 9
      |> Verify

[<Scenario>]     
let ``Given 3, 4, 3, 4, 5 placed on "three of a kind" gives 0.`` () =   
    Given (3, 4, 3, 4, 5)
      |> When (placed_on ThreeOfAKind)
      |> It should equal 0
      |> Verify