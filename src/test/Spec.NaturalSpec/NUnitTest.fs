module NaturalSpec.NUnitTest

[<Scenario>]
let Foo2() =
  Given true
    |> It should equal true
    |> Verify