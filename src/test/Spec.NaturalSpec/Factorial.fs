module NaturalSpec.Factorial

let rec fac = function 
| 0 -> 1
| x -> x * fac (x - 1)


let factorial x = 
    printMethod x  ///  prints the method name via reflection
    fac x
              
[<Scenario>]
let ``Factorial of 0 = 0``() =
  Given 0
    |> When calculating factorial
    |> It should equal 1
    |> Verify    

[<Scenario>]
let ``Factorial of 1 = 1``() =
  Given 1
    |> When calculating factorial
    |> It should equal 1
    |> Verify    

[<Scenario>]
let ``Factorial of 5 = 120``() =
  Given 5
    |> When calculating factorial
    |> It should equal 120
    |> Verify    

// shorter with scenario templates
[<ScenarioTemplate(0,1)>]
[<ScenarioTemplate(1,1)>]
[<ScenarioTemplate(5,120)>]
let ``Factorial of n = m``(n,m) =
  Given n
    |> When calculating factorial
    |> It should equal m
    |> Verify    