module Probability.Specs

open NaturalSpec
open Model

let combined_with x y=
    printMethod ""
    combine x y

let inversed x=
    printMethod ""
    inverse x

[<Scenario>]     
let ``Certainly combined with Certainly should be Certainly`` () =   
    Given Certainly
      |> When combined_with Certainly
      |> It should equal Certainly
      |> Verify

[<Scenario>]     
let ``Certainly combined with Impossible should be Impossible`` () =   
    Given Certainly
      |> When combined_with Impossible
      |> It should equal Impossible
      |> Verify

[<Scenario>]     
let ``Impossible combined with Certainly should be Impossible`` () =   
    Given Impossible
      |> When combined_with Certainly
      |> It should equal Impossible
      |> Verify

let oneHalf = toProbability <| 1N/2N
let oneThird = toProbability <| 1N/3N
let twoThirds = toProbability <| 2N/3N

[<Scenario>]     
let ``One half combined with Certainly should be one half`` () =   
    Given oneHalf
      |> When combined_with Certainly
      |> It should equal oneHalf
      |> Verify

[<Scenario>]     
let ``One half combined with two thirds should be one third`` () =   
    Given oneHalf
      |> When combined_with twoThirds
      |> It should equal oneThird
      |> Verify

[<Scenario>]     
let ``The inverse of Impossible should be Certainly`` () =
    Given Impossible
      |> When inversed
      |> It should equal Certainly
      |> Verify
