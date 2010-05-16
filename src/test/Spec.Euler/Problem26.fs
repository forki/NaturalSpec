module Euler.Problem26
  
open NaturalSpec
open System.Collections.Generic

// Problem 26
// A unit fraction contains 1 in the numerator. The decimal representation of the unit 
// fractions with denominators 2 to 10 are given:
// 
// 1/2	 = 	0.5
// 1/3	 = 	0.(3)
// 1/4	 = 	0.25
// 1/5	 = 	0.2
// 1/6	 = 	0.1(6)
// 1/7	 = 	0.(142857)
// 1/8	 = 	0.125
// 1/9	 = 	0.(1)
// 1/10 = 	0.1
// 
// Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. It can be seen that 1/7 has a 6-digit recurring cycle.
// 
// Find the value of d<1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.


let getPeriod a b =
    let rec divide map step n =    
        match Map.tryFind n map with
        | Some value -> step - value
        | _ ->
            let m = Map.add n step map
            match n with
            | _ when n = 0 -> divide m step 0
            | _ when n = b -> divide m step 0
            | _ when n > b -> divide m (step + 1) (n % b)
            | _  -> divide m step (n * 10)

    divide Map.empty 0 a

let PeriodLength n =
    printMethod ""
    getPeriod 1 n

let MaxPeriodLength n =
    printMethod ""
    seq {1..n}
      |> Seq.maxBy (getPeriod 1)

[<ScenarioTemplate(2,0)>]
[<ScenarioTemplate(3,1)>]
[<ScenarioTemplate(4,0)>]
[<ScenarioTemplate(5,0)>]
[<ScenarioTemplate(6,1)>]
[<ScenarioTemplate(7,6)>]
[<ScenarioTemplate(8,0)>]
[<ScenarioTemplate(9,1)>]
[<ScenarioTemplate(10,0)>]
let ``What is the the period length of 1/n?`` (n,periodLength) =
    Given n
      |> When calculating PeriodLength
      |> It should equal periodLength
      |> Verify

[<ScenarioTemplate(10,7)>]
[<ScenarioTemplate(1000,983)>]
let ``What is the value of d smaller than n for which 1/d contains the longest recurring cycle in its decimal fraction part?`` (n,result) =
    Given n
      |> When calculating MaxPeriodLength
      |> It should equal result
      |> Verify
      