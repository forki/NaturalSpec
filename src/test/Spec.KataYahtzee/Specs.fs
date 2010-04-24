module Yahtzee.Specs

open NaturalSpec
open Model

let placed_on category list =
    printMethod category
    calcValue category list

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

// Four of a kind: If there are four dice with the same number, 
// the player scores the sum of these dice. Otherwise, the player scores 0. 
// For example, 2, 2, 2, 2, 5 places on "four of a kind" gives 8.

[<Scenario>]     
let ``Given 2, 2, 2, 2, 5 placed on "tour of a kind" gives 8.`` () =   
    Given (2, 2, 2, 2, 5)
      |> When (placed_on FourOfAKind)
      |> It should equal 8
      |> Verify

[<Scenario>]     
let ``Given 2, 6, 2, 2, 5 placed on "tour of a kind" gives 0.`` () =   
    Given (2, 6, 2, 2, 5)
      |> When (placed_on FourOfAKind)
      |> It should equal 0
      |> Verify

// Small straight: If the dice read 1,2,3,4,5, the player scores 15 (the sum of all the dice), 
// otherwise 0.

[<Scenario>]     
let ``Given 1,2,3,4,5 placed on "Small Straight" gives 15.`` () =   
    Given (1,2,3,4,5)
      |> When (placed_on SmallStraight)
      |> It should equal 15
      |> Verify

[<Scenario>]     
let ``Given 1,2,5,4,3 placed on "Small Straight" gives 15.`` () =   
    Given (1,2,5,4,3)
      |> When (placed_on SmallStraight)
      |> It should equal 15
      |> Verify

[<Scenario>]     
let ``Given 1,2,6,4,3 placed on "Small Straight" gives 0.`` () =   
    Given (1,2,6,4,3)
      |> When (placed_on SmallStraight)
      |> It should equal 0
      |> Verify

// Large straight: If the dice read 2,3,4,5,6, the player scores 20 (the sum of all the dice),
//  otherwise 0.

[<Scenario>]     
let ``Given 2,3,4,5,6 placed on "Large Straight" gives 20.`` () =   
    Given (2,3,4,5,6)
      |> When (placed_on LargeStraight)
      |> It should equal 20
      |> Verify

[<Scenario>]     
let ``Given 6,2,5,4,3 placed on "Large Straight" gives 20.`` () =   
    Given (6,2,5,4,3)
      |> When (placed_on LargeStraight)
      |> It should equal 20
      |> Verify

[<Scenario>]     
let ``Given 1,2,6,4,3 placed on "Large Straight" gives 0.`` () =   
    Given (1,2,6,4,3)
      |> When (placed_on LargeStraight)
      |> It should equal 0
      |> Verify

// Full house: If the dice are two of a kind and three of a kind, 
// the player scores the sum of all the dice. 
// For example, 1,1,2,2,2 placed on "full house" gives 8. 4,4,4,4,4 is not "full house".

[<Scenario>]     
let ``Given 1,1,2,2,2 placed on "full house" gives 8.`` () =   
    Given (1,1,2,2,2)
      |> When (placed_on FullHouse)
      |> It should equal 8
      |> Verify

[<Scenario>]     
let ``Given 4,4,4,4,4 placed on "full house" gives 0.`` () =   
    Given (4,4,4,4,4)
      |> When (placed_on FullHouse)
      |> It should equal 0
      |> Verify

[<Scenario>]     
let ``Given 1,1,2,3,2 placed on "full house" gives 0.`` () =   
    Given (1,1,2,3,2)
      |> When (placed_on FullHouse)
      |> It should equal 0
      |> Verify