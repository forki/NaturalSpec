module NaturalSpec.WhereasSpec

open NaturalSpec

[<Scenario>]
let When_using_Whereas() =
  let c = ref 0
  let f x =
     c := 1
     x

  Given true
    |> When calculating f
    |> It should equal true
    |> Whereas (!c)
    |> It should equal 1
    |> Verify  