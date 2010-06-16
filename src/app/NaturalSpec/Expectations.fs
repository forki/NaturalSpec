module NaturalSpec.Expectations

open System.Diagnostics

/// Try to find scenario in call stack
let findScenario() =
    let rec findScenario i = 
        let m = (new StackTrace()).GetFrame(i).GetMethod()
        if m.GetCustomAttributes(typeof<Scenario>,true).Length > 0 ||
           m.GetCustomAttributes(typeof<ScenarioTemplate>,true).Length > 0
        then m else findScenario (i+1)
  
    try 
        findScenario 1 
    with ex -> 
        (new StackTrace()).GetFrame(2).GetMethod()
  
let getScenarioName () =
    findScenario().Name.Replace("_"," ")


/// All expectations
let private expectations = new System.Collections.Generic.List<string * (unit -> System.Exception)>()

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