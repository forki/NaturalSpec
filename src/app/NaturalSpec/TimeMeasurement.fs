module NaturalSpec.TimeMeasurement
open System.Diagnostics

let stopWatch = new Stopwatch()
  
let printElapsed() = 
    sprintf "\n  ==> Time: %.4fs\n" stopWatch.Elapsed.TotalSeconds 
      |> Utils.toSpec  