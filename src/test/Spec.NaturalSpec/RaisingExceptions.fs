module NaturalSpec.RaisingExceptions

let raising x =
  printfn "raising exception %A" x
  failwith x     
    
[<Scenario>]
[<FailsWith "My error">]
let ``Raising exception``() =
  Given 0
    |> When raising "My error"
    |> Verify

[<Scenario>]
[<FailsWithType (typeof<System.DivideByZeroException>)>]
let ``Dividing by zero should fail with DivideByZeroException``() =
  Given 10
    |> When dividing_by 0
    |> Verify 