module Spec.NaturalSpec.NUnitTest

open NUnit.Framework
open NaturalSpec

[<Scenario>]
let Foo2() =
  Given true
    |> It should equal true
    |> Verify