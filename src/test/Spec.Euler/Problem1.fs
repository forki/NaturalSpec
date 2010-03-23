module Problem1 
  
open NaturalSpec

let SumOfMultiplesOf3And5 max = 
    printMethod ""
    [3..max-1] 
      |> List.filter (fun x -> x % 3 = 0 || x % 5 = 0)
      |> List.sum

[<Scenario>]     
let Problem1__WhenGettingSumOfMultiplesOf3And5ToAMaxNumberOf10 () =   
    Given 10
      |> When solving SumOfMultiplesOf3And5
      |> It should equal 23
      |> Verify