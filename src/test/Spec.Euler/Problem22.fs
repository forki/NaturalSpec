module Problem22
  
open NaturalSpec

// Problem 22
// Using names.txt, a 46K text file containing over five-thousand first names, 
// begin by sorting it into alphabetical order. 
// Then working out the alphabetical value for each name, 
// multiply this value by its alphabetical position in the list to obtain a name score.
//
// For example, when the list is sorted into alphabetical order, 
// COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. 
// So, COLIN would obtain a score of 938 × 53 = 49714.
//
// What is the total of all the name scores in the file?

let names =
     System.IO.File.ReadAllLines "names.txt"
       |> Array.map (fun line -> line.Split [| ',' |] |> Array.map (fun s -> s.Trim [|'\"'|]))
       |> Array.concat
       |> Array.sort

let Position name =
    printMethod ""
    Array.findIndex ((=) name) names + 1

let chars = [|'A'..'Z'|]
let calcCharValue c = Array.findIndex ((=)c) chars + 1
let calcNameCharValue = Seq.sumBy calcCharValue

let CharValueSum n =
    printMethod ""
    calcNameCharValue n

let NamesScoreSum names =
    printMethod ""
    names
      |> Seq.mapi (fun i name -> (i+1) * calcNameCharValue name)
      |> Seq.sum

[<Scenario>]      
let ``What is the position of COLIN?``() =
    Given "COLIN"
      |> When calculating Position
      |> It should equal 938
      |> Verify

[<Scenario>]      
let ``What is the char value sum of COLIN?``() =
    Given "COLIN"
      |> When calculating CharValueSum
      |> It should equal 53
      |> Verify

[<Scenario>]      
let ``What is the total of all the name scores in the file?``() =
    Given names
      |> When calculating NamesScoreSum
      |> It should equal 871198282
      |> Verify