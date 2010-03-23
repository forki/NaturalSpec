module NaturalSpec.Factorial

let rec fac = function 
| 0 -> 1
| x -> x * fac (x - 1)


let factorial x = 
    printMethod x  ///  prints the method name via reflection
    fac x

[<Scenario>]
let When_calculating_fac_5_it_should_equal_120() =
  Given 5
    |> When calculating factorial
    |> It should equal 120
    |> Verify    
    
[<Scenario>]
let When_calculating_fac_1_it_should_equal_1() =
  Given 1
    |> When calculating factorial
    |> It should equal 1
    |> Verify          
    
[<Scenario>]
let When_calculating_fac_0_it_should_equal_0() =
  Given 0
    |> When calculating factorial
    |> It should equal 1
    |> Verify    