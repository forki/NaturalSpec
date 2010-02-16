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