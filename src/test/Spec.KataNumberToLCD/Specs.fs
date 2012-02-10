module KataNumberToLCD.Specs

open NaturalSpec

open Model

let converting_to_LCD number =
    printMethod ""
    toLCD number

[<Scenario>]     
let ``Should display single 0`` () =   
    Given 0
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "| |\n" +
             "   \n" +
             "| |\n" +
             " - ")
      |> Verify

[<Scenario>]     
let ``Should display single 1`` () =   
    Given 1
      |> When converting_to_LCD
      |> It should equal 
            ("   \n" +
             "  |\n" +
             "   \n" +
             "  |\n" +
             "   ")
      |> Verify

[<Scenario>]     
let ``Should display single 2`` () =   
    Given 2
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "  |\n" +
             " - \n" +
             "|  \n" +
             " - ")
      |> Verify

[<Scenario>]     
let ``Should display single 3`` () =   
    Given 3
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "  |\n" +
             " - \n" +
             "  |\n" +
             " - ")
      |> Verify

[<Scenario>]     
let ``Should display single 4`` () =   
    Given 4
      |> When converting_to_LCD
      |> It should equal 
            ("   \n" +
             "| |\n" +
             " - \n" +
             "  |\n" +
             "   ")
      |> Verify

[<Scenario>]     
let ``Should display single 5`` () =   
    Given 5
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "|  \n" +
             " - \n" +
             "  |\n" +
             " - ")
      |> Verify

[<Scenario>]     
let ``Should display single 6`` () =   
    Given 6
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "|  \n" +
             " - \n" +
             "| |\n" +
             " - ")
      |> Verify

[<Scenario>]     
let ``Should display single 7`` () =   
    Given 7
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "  |\n" +
             "   \n" +
             "  |\n" +
             "   ")
      |> Verify

[<Scenario>]     
let ``Should display single 8`` () =   
    Given 8
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "| |\n" +
             " - \n" +
             "| |\n" +
             " - ")
      |> Verify

[<Scenario>]     
let ``Should display single 9`` () =   
    Given 9
      |> When converting_to_LCD
      |> It should equal 
            (" - \n" +
             "| |\n" +
             " - \n" +
             "  |\n" +
             " - ")
      |> Verify

[<Scenario>]     
let ``Should display two digits`` () =   
    Given 12
      |> When converting_to_LCD
      |> It should equal 
            ("     - \n" +
             "  |   |\n" +
             "     - \n" +
             "  | |  \n" +
             "     - ")
      |> Verify

[<Scenario>]     
let ``Should display three digits`` () =   
    Given 345
      |> When converting_to_LCD
      |> It should equal 
            (" -       - \n" +
             "  | | | |  \n" +
             " -   -   - \n" +
             "  |   |   |\n" +
             " -       - ")
      |> Verify

[<Scenario>]     
let ``Should display five digits`` () =   
    Given 67890
      |> When converting_to_LCD
      |> It should equal 
            (" -   -   -   -   - \n" +
             "|     | | | | | | |\n" +
             " -       -   -     \n" +
             "| |   | | |   | | |\n" +
             " -       -   -   - ")
      |> Verify