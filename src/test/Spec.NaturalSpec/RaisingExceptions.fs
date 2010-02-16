module NaturalSpec.RaisingExceptions

let raising x =
  printfn "raising exception %A" x
  failwith x     
    
[<Scenario>]
[<FailsWith "My error">]
let When_raising_exception() =
  Given 0
    |> When raising "My error"
    |> Verify

[<Scenario>]
[<FailsWithType (typeof<System.DivideByZeroException>)>]
let When_dividing_by_zero_it_should_fail() =
  Given 10
    |> When dividing_by 0
    |> Verify 