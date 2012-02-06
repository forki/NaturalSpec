module KataNumberToLCD.Specs

open NaturalSpec

open Model

let converting_to_LCD number =
    printMethod ""
    toLCD number

[<Scenario>]     
let ``Should display single 0`` () =   
    Given "0"
      |> When converting_to_LCD
      |> It should equal 
            (" _ \n" +
             "| |\n" +
             "|_|\n")
      |> Verify