[<AutoOpen>]
module NaturalSpec.Utils

open System.IO
open System.Collections.Generic
open System.Diagnostics

let maxOutputLength = 70

let prepareOutput (x:obj) =
  let s = sprintf "%A" x
  if s.Length > maxOutputLength then s.Substring(0,maxOutputLength) + "..." else s
  
/// Internal type of Assertion
type AssertType =
| Equality
| Inequality
| IsTrue
| IsFalse            
  

/// Streamwriter for spec output
let specWriter =
  let rec getFileName i =
    let fileName = sprintf "Spec_%d.txt" i
    if (new FileInfo(fileName)).Exists then
      getFileName (i+1)
    else
      fileName
      
  let file = new FileStream(getFileName 1, FileMode.Create, FileAccess.Write)
  new StreamWriter(file)
  
specWriter.AutoFlush <- true

/// Writes the given string to the spec output
let toSpec s =
  printf "%s" s
  specWriter.Write s
  
  
/// Prints the test scenario name to the spec output         
let printScenario() = 
  /// Try to find scenario in call stack
  let rec findScenario i = 
    let m = (new StackTrace()).GetFrame(i).GetMethod()
    if m.GetCustomAttributes(typeof<Scenario>,true).Length > 0 ||
       m.GetCustomAttributes(typeof<ScenarioTemplate>,true).Length > 0
    then m else findScenario (i+1)
  
  let m = 
    try 
      findScenario 1 
    with ex -> 
      (new StackTrace()).GetFrame(2).GetMethod()
  
  let methodName = m.Name.Replace("_"," ")
    
  sprintf "\n\nScenario: %s\n" methodName |> toSpec
  for attrib in m.GetCustomAttributes(typeof<Fails>,true) do
    let a = attrib :?> Fails
    match a.ExpectedMessage , a.ExpectedException with
    | null, null
    | ""  , null -> "  - Should fail with unspecified exception\n" |> toSpec
    | m   , null -> sprintf "  - Should fail with %A\n" m |> toSpec
    | null, t  
    | "", t      -> sprintf "  - Should fail with exception type %A\n" t |> toSpec
    | x, t       -> sprintf "  - Should fail with %A and message %A\n" t m |> toSpec
  
open NUnit.Framework

/// Checks if the given condition is valid
let check x = 
  let assertType,(a:obj),b,value = x
  match assertType with
  | Equality -> 
     if a <> b then
       let s = sprintf "Elements are not equal.\nExpected:%s\nBut was: %s\n" (prepareOutput a) (prepareOutput b)
       s |> toSpec  
       Assert.Fail s 
  | Inequality -> 
     if a = b then
       let s = sprintf "Elements should not be equal.\nBut both are: %s\n" (prepareOutput a)
       s |> toSpec  
       Assert.Fail s      
  | IsTrue -> Assert.AreEqual(a,b)
  | IsFalse -> Assert.AreNotEqual(a,b)
  value      
  
/// Negotiates a observation
let not' x = 
  let aType,a,b,value = x
  match aType with
  | Equality -> Inequality,a,b,value
  | Inequality -> Equality,a,b,value      
  | IsTrue -> IsFalse,a,b,value
  | IsFalse -> IsTrue,a,b,value        
    