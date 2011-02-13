module NaturalSpec.Expectations

open System.Diagnostics
open System.Collections.Generic

/// Try to find scenario in call stack
let findScenario() =
    let rec findScenario i = 
        let m = (new StackTrace()).GetFrame(i).GetMethod()
        if m.GetCustomAttributes(typeof<Scenario>,true).Length > 0 ||
           m.GetCustomAttributes(typeof<Example>,true).Length > 0
        then m else findScenario (i+1)
  
    try 
        findScenario 1 
    with ex -> 
        (new StackTrace()).GetFrame(2).GetMethod()
  
let getScenarioName () =
    findScenario().Name.Replace("_"," ")


/// All expectations
let private expectations = new List<string * (unit -> System.Exception)>()
let internal calls = new List<string * string * string * obj>()

let addCall scenario mock methodName param = calls.Add (scenario,mock,methodName,param)
let add = expectations.Add

let getErrors () =
    expectations.Reverse()
    let scenario = getScenarioName() 
    let errors =
        expectations
          |> Seq.filter (fun (s,f) -> s = scenario)
          |> Seq.map (fun (s,f) -> f())
          |> Seq.filter ((<>) null)
          |> Seq.toList
    expectations.Clear()
    errors

let clear = expectations.Clear