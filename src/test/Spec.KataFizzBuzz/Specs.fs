module FizzBuzz.Specs

open NaturalSpec

let fizzbuzz n =
    let t = sprintf "%d" n
    let s1 = if n % 3 = 0 || t.Contains("3") then "fizz" else ""
    let s2 = if n % 5 = 0 || t.Contains("5") then "buzz" else ""
    let s = s1 + s2
    if s = "" then t else s

let fizzBuzz n =
  printMethod ""
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
[<ScenarioTemplate(48,"fizz")>]
[<ScenarioTemplate(60,"fizzbuzz")>]
let ``Given n gives simple fizzbuzz result.`` (n,result) =   
    Given n
      |> When fizzBuzz
      |> It should equal result
      |> Verify

[<ScenarioTemplate(31,"fizz")>]
[<ScenarioTemplate(33,"fizz")>]
[<ScenarioTemplate(35,"fizzbuzz")>]
[<ScenarioTemplate(39,"fizz")>]
[<ScenarioTemplate(51,"fizzbuzz")>]
[<ScenarioTemplate(52,"buzz")>]
[<ScenarioTemplate(59,"buzz")>]
let ``Given n gives extended fizzbuzz result.`` (n,result) =   
    Given n
      |> When fizzBuzz
      |> It should equal result
      |> Verify