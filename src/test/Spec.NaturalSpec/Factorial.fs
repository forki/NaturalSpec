module NaturalSpec.Factorial

let factorial x = 
  printMethod ""  ///  prints the method name via reflection
  let rec fac_ x =
    match x with
    | 0 -> 1
    | x -> x * fac_ (x - 1)
  fac_ x

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