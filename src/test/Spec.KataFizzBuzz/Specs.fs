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

[<Example(1,"1")>] 
[<Example(2,"2")>]  
[<Example(3,"fizz")>]
[<Example(4,"4")>]
[<Example(5,"buzz")>]
[<Example(6,"fizz")>]
[<Example(7,"7")>]
[<Example(8,"8")>]
[<Example(9,"fizz")>]
[<Example(10,"buzz")>]
[<Example(15,"fizzbuzz")>]
[<Example(33,"fizz")>]
[<Example(48,"fizz")>]
[<Example(60,"fizzbuzz")>]
let ``Given n gives simple fizzbuzz result.`` (n,result) =   
    Given n
      |> When fizzBuzz
      |> It should equal result
      |> Verify

[<Example(31,"fizz")>]
[<Example(33,"fizz")>]
[<Example(35,"fizzbuzz")>]
[<Example(39,"fizz")>]
[<Example(51,"fizzbuzz")>]
[<Example(52,"buzz")>]
[<Example(59,"buzz")>]
let ``Given n gives extended fizzbuzz result.`` (n,result) =   
    Given n
      |> When fizzBuzz
      |> It should equal result
      |> Verify