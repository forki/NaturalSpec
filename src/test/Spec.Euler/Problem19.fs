module Euler.Problem19

open NaturalSpec

// Problem 19
// How many Sundays fell on the first of the month during the twentieth century 
// (1 Jan 1901 to 31 Dec 2000)?

let date d m y  = new System.DateTime(y,m,d)

let datesAfter startDate= 
    Seq.unfold (fun (actDate:System.DateTime) -> Some (actDate,actDate.AddDays(1.0))) startDate   

let CountFirstSundaysUntil endDate startDate =
    printMethod endDate
    datesAfter startDate
      |> Seq.filter (fun d -> d.DayOfWeek = System.DayOfWeek.Sunday && d.Day = 1) 
      |> Seq.takeWhile (fun d -> d <= endDate)
      |> Seq.length

[<Scenario>]      
let ``How many Sundays fell on the first of the month during the twentieth century?``() =
    Given (date 1 1 1901)
      |> When solving (CountFirstSundaysUntil (date 31 12 2000))
      |> It should equal 171
      |> Verify
