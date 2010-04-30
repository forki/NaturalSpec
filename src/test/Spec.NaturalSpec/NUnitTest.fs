module NaturalSpec.NUnitTest

[<Scenario>]
let ``Testing if NUnit is running correctly``() =
  Given true
    |> It should equal true
    |> Verify