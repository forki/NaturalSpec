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

//[<Fact>]
//let should_remove_placeholders_when_key_was_not_found () =
//  let text = subst "$say$ $me$" [("who", "bingo");("say", "hello")]
//  Assert.Equal<string>("hello ", text)