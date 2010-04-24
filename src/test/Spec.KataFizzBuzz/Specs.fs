module FizzBuzz.Specs

open NaturalSpec

let fizzbuzz n =
  sprintf "%d" n

let fizzBuzz n =
  printMethod n
  fizzbuzz n

[<Scenario>]     
let ``Given 1 gives 1.`` () =   
    Given 1
      |> When fizzBuzz
      |> It should equal "1"
      |> Verify

[<Scenario>]     
let ``Given 2 gives 2.`` () =   
    Given 2
      |> When fizzBuzz
      |> It should equal "2"
      |> Verify