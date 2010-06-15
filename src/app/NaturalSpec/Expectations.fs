module NaturalSpec.Expectations

/// All expectations
let private expectations = new System.Collections.Generic.List<unit -> System.Exception>()

let add = expectations.Add

let getErrors () =
    expectations.Reverse()
    let errors =
        expectations
          |> Seq.map (fun f -> f())
          |> Seq.filter ((<>) null)
          |> Seq.toList
    expectations.Clear()
    errors

let clear = expectations.Clear