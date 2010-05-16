module Problem48
  
open NaturalSpec

// Problem 48
// The series, 1^(1) + 2^(2) + 3^(3) + ... + 10^(10) = 10405071317.
//
// Find the last ten digits of the series, 1^(1) + 2^(2) + 3^(3) + ... + 1000^(1000).

let powerSeries n = seq {1..n} |> Seq.sumBy (fun x -> pow (bigint x) x)

let PowerSeries n =
    printMethod ""
    powerSeries n    

let LastTenDigitsOfPowerSeries n =
    printMethod ""
    powerSeries n
      |> fun x -> x.ToString()
      |> fun x -> x.Substring(x.Length - 10)

[<Scenario>]      
let ``Find 1^(1) + 2^(2) + 3^(3) + ... + 10^(10).``() =
    Given 10
      |> When calculating PowerSeries
      |> It should equal 10405071317I
      |> Verify

[<Scenario>]      
let ``Find the last ten digits of the series, 1^(1) + 2^(2) + 3^(3) + ... + 1000^(1000).``() =
    Given 1000
      |> When calculating LastTenDigitsOfPowerSeries
      |> It should equal "9110846700"
      |> Verify