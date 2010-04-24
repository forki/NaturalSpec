module FizzBuzz.Specs

open NaturalSpec

let fizzbuzz n =
    let s1 = if n % 3 = 0 then "fizz" else ""
    let s2 = if n % 5 = 0 then "buzz" else ""
    let s = s1 + s2
    if s = "" then sprintf "%d" n else s

let fizzBuzz n =
  printMethod n
  fizzbuzz n

[<ScenarioTemplate(1,"1")>] 
[<ScenarioTemplate(2,"2")>]  
[<ScenarioTemplate(3,"fizz")>]
[<ScenarioTemplate(4,"4")>]
[<ScenarioTemplate(5,"buzz")>]
[<ScenarioTemplate(6,"fizz")>]
[<ScenarioTemplate(7,"7")>]
[<ScenarioTemplate(8,"8")>]
[<ScenarioTemplate(9,"fizz")>]
[<ScenarioTemplate(10,"buzz")>]
[<ScenarioTemplate(15,"fizzbuzz")>]
[<ScenarioTemplate(33,"fizz")>]
[<ScenarioTemplate(34,"34")>]
[<ScenarioTemplate(60,"fizzbuzz")>]
let ``Given n gives result.`` (n,result) =   
    Given n
      |> When fizzBuzz
      |> It should equal result
      |> Verify