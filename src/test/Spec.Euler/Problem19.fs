module Problem19

open NaturalSpec

// Problem 19
// How many Sundays fell on the first of the month during the twentieth century 
// (1 Jan 1901 to 31 Dec 2000)?

let date d m y  = new System.DateTime(y,m,d)

let dates (startDate:System.DateTime) endDate = 
    let actDate = ref startDate
    seq {
        while !actDate <= endDate do
            yield !actDate
            actDate := (!actDate).AddDays(1.0) }

let CountFirstSundaysUntil endDate startDate =
    printMethod endDate
    dates startDate endDate
      |> Seq.filter (fun d -> d.DayOfWeek = System.DayOfWeek.Sunday && d.Day = 1) 
      |> Seq.length

[<Scenario>]      
let ``How many Sundays fell on the first of the month during the twentieth century?``() =
    Given (date 1 1 1901)
      |> When solving (CountFirstSundaysUntil (date 31 12 2000))
      |> It should equal 171
      |> Verify
