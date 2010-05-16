module Euler.Problem17
  
open NaturalSpec

// Problem 17
// If the numbers 1 to 5 are written out in words: one, two, three, four, five, 
// then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.
// 
// If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, 
// how many letters would be used?
// 
// NOTE: Do not count spaces or hyphens. 
// For example, 342 (three hundred and forty-two) contains 23 letters and 
// 115 (one hundred and fifteen) contains 20 letters. 
// The use of "and" when writing out numbers is in compliance with British usage.

/// Single-digit and small number names
let smallNumbers =
    [| "Zero"; "One"; "Two"; "Three"; "Four"; "Five"; "Six"; "Seven"; "Eight";
       "Nine"; "Ten"; "Eleven"; "Twelve"; "Thirteen"; "Fourteen"; "Fifteen";
       "Sixteen"; "Seventeen"; "Eighteen"; "Nineteen" |]
     
/// Tens number names from twenty upwards
let tens = [| ""; ""; "Twenty"; "Thirty"; "Forty"; "Fifty"; "Sixty"; "Seventy"; "Eighty"; "Ninety" |]
     
// Scale number names for use during recombination
let scaleNumbers = [ ""; "Thousand"; "Million"; "Billion" ]

let inline append guard text s = if guard then s + text else s

/// Converts a three-digit group into English words
let threeDigitGroupToWords threeDigits =
    // Determine the hundreds and the remainder
    let hundreds = threeDigits / 100
    let tensUnits = threeDigits % 100

    // Hundreds rules
    let hundredsText =
        if hundreds = 0 then "" else
        smallNumbers.[hundreds] + " Hundred" 
          |> append (tensUnits <> 0) " and "

    // Determine the tens and units
    let t = tensUnits / 10
    let units = tensUnits % 10

    // Tens rules
    hundredsText
      + 
        if t >= 2 then
            append (units <> 0) ("-" + smallNumbers.[units]) tens.[t]         
        else        
            if tensUnits <> 0 then smallNumbers.[tensUnits] else ""


let writtenNumber n =
    if n = 0 then smallNumbers.[0] else
    let n' = abs n
    let x1 = n' % 1000
    let startText = threeDigitGroupToWords x1
    
    Seq.init 3 (fun i -> n' / ((i+1)*1000) % 1000)
      |> Seq.filter ((<>) 0)
      |> Seq.mapi (fun i x -> i,threeDigitGroupToWords x)
      |> Seq.fold (fun (combined,appendAnd) (i,text) ->
              let s =
                  text + " " + scaleNumbers.[i+1] +
                     if isNullOrEmpty combined then "" else 
                     if appendAnd then " and " else ", "
              s + combined,false)
          (startText,x1 > 0 && x1 < 100)
      |> fst
      |> fun x -> if n < 0 then "Negative " + x else x
   
let writtenWordLength n =
    writtenNumber n
      |> fun x -> x.Replace(" ","").Replace("-","").Length
 
let WrittenWord n =
    printMethod ""
    writtenNumber n
      |> fun x -> x.ToLower()

let WrittenWordLength n =
    printMethod ""
    writtenWordLength n

let CharCountOfWrittenWordsUpTo n =
    printMethod ""
    seq { 1 .. n}
      |> Seq.map writtenWordLength
      |> Seq.sum

[<ScenarioTemplate(0,"zero")>]
[<ScenarioTemplate(1,"one")>]
[<ScenarioTemplate(2,"two")>]
[<ScenarioTemplate(3,"three")>]
[<ScenarioTemplate(4,"four")>]
[<ScenarioTemplate(5,"five")>]
[<ScenarioTemplate(6,"six")>]
[<ScenarioTemplate(7,"seven")>]
[<ScenarioTemplate(8,"eight")>]
[<ScenarioTemplate(9,"nine")>]
[<ScenarioTemplate(10,"ten")>]
[<ScenarioTemplate(11,"eleven")>]
[<ScenarioTemplate(12,"twelve")>]
[<ScenarioTemplate(13,"thirteen")>]
[<ScenarioTemplate(14,"fourteen")>]
[<ScenarioTemplate(342,"three hundred and forty-two")>]
[<ScenarioTemplate(115,"one hundred and fifteen")>]
[<ScenarioTemplate(1000,"one thousand")>]
let ``What is the written word for the number n?`` (n,word) =
    Given n
      |> When calculating WrittenWord
      |> It should equal word
      |> Verify

[<ScenarioTemplate(1,3)>]
[<ScenarioTemplate(2,3)>]
[<ScenarioTemplate(3,5)>]
[<ScenarioTemplate(4,4)>]
[<ScenarioTemplate(5,4)>]
[<ScenarioTemplate(342,23)>]
[<ScenarioTemplate(115,20)>]
let ``What is the length of the written word for the number n?`` (n,length) =
    Given n
      |> When calculating WrittenWordLength
      |> It should equal length
      |> Verify

[<ScenarioTemplate(1,3)>]
[<ScenarioTemplate(2,6)>]
[<ScenarioTemplate(5,19)>]
[<ScenarioTemplate(1000,21124)>]
let ``If all the numbers from 1 to n inclusive were written out in words, how many letters would be used??`` (n,length) =
    Given n
      |> When calculating CharCountOfWrittenWordsUpTo
      |> It should equal length
      |> Verify