module StringReplacer.Specs

open NaturalSpec

open Model

let replacing replacements =
    printMethod replacements
    replace replacements


[<Scenario>]     
let ``Should yield empty text when empty text is provided`` () =   
    Given ""
      |> When replacing []
      |> It should equal ""
      |> Verify

[<Scenario>]     
let ``Should yield text when text is passed with no keywords`` () =   
    Given "something"
      |> When replacing []
      |> It should equal "something"
      |> Verify

[<Scenario>]     
let ``Should replace key with value when key was found`` () =   
    Given "hi $who$"
      |> When replacing ["who","bingo"]
      |> It should equal "hi bingo"
      |> Verify

[<Scenario>]     
let ``Should replace multiple keys with values when found`` () =   
    Given "$say$ $who$"
      |> When replacing ["who","bingo"; "say","hello"]
      |> It should equal "hello bingo"
      |> Verify

[<Scenario>]     
let ``Should remove placeholders when key was not found`` () =   
    Given "$say$ $me$"
      |> When replacing ["who","bingo"; "say","hello"]
      |> It should equal "hello "
      |> Verify

[<Scenario>]     
let ``Should remove multiple placeholders when key was not found`` () =   
    Given "$say$ $me$$really$ $who$"
      |> When replacing ["who","bingo"; "say","hello"]
      |> It should equal "hello  bingo"
      |> Verify

let searching_for_template text =
    printMethod ""
    findTemplate text

[<Scenario>]     
let ``Should not find template in string without $`` () =   
    Given "hello bingo"
      |> When searching_for_template
      |> It should equal None
      |> Verify

[<Scenario>]     
let ``Should not find template in string with only one $`` () =   
    Given "hello $bingo"
      |> When searching_for_template
      |> It should equal None
      |> Verify