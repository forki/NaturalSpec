module Sort.Specs

open NaturalSpec

open Model

let sorting list =
    printMethod ""
    sort list

[<Scenario>]     
let ``When sorting an empty list it should return an empty list`` () =   
    Given []
      |> When sorting
      |> It should equal []
      |> Verify
      
[<Scenario>]     
let ``When sorting a singleton list it should return the same list`` () =   
    Given [1]
      |> When sorting
      |> It should equal [1]
      |> Verify

[<Scenario>]     
let ``When sorting a sorted list with two elements it should return the same list`` () =   
    Given [1;2]
      |> When sorting
      |> It should equal [1;2]
      |> Verify

[<Scenario>]     
let ``When sorting an unsorted list with two elements`` () =   
    Given [2;1]
      |> When sorting
      |> It should equal [1;2]
      |> Verify

[<Scenario>]     
let ``When sorting a sorted list with three elements it should return the same list`` () =   
    Given [1;2;3]
      |> When sorting
      |> It should equal [1;2;3]
      |> Verify

[<Scenario>]     
let ``When sorting an unsorted list with three elements`` () =   
    Given [2;1;3]
      |> When sorting
      |> It should equal [1;2;3]
      |> Verify

[<Scenario>]     
let ``When sorting an other unsorted list with three elements`` () =   
    Given [1;3;2]
      |> When sorting
      |> It should equal [1;2;3]
      |> Verify

[<Scenario>]     
let ``When sorting a reverse sorted list with three elements`` () =   
    Given [3;2;1]
      |> When sorting
      |> It should equal [1;2;3]
      |> Verify

[<Scenario>]     
let ``When sorting a list with three equal elements`` () =   
    Given [4;4;4]
      |> When sorting
      |> It should equal [4;4;4]
      |> Verify

[<Scenario>]     
let ``When sorting a list with 10 elements`` () =   
    Given [4;5;4;-3;2;1;8;0;9;1]
      |> When sorting
      |> It should equal [-3; 0; 1; 1; 2; 4; 4; 5; 8; 9]
      |> Verify