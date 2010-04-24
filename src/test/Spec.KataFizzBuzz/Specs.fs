module FizzBuzz.Specs

open NaturalSpec

let fizzbuzz n =
    if n % 3 = 0 then "fizz" else
    sprintf "%d" n

let fizzBuzz n =
  printMethod n
  fizzbuzz n

[<ScenarioTemplate(1,"1")>] 
[<ScenarioTemplate(2,"2")>]  
[<ScenarioTemplate(3,"fizz")>]
[<ScenarioTemplate(4,"4")>]
let ``Given n gives result.`` (n,result) =   
    Given n
      |> When fizzBuzz
      |> It should equal result
      |> Verify