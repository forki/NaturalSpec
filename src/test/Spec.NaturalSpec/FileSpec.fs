module Spec.NaturalSpec.FileSpec

open NUnit.Framework
open NaturalSpec
open System.Xml

let save (fileName:string) =
  let doc = new XmlDocument()
  doc.CreateElement "element" 
    |> doc.AppendChild 
    |> ignore
  doc.Save fileName
  
let save2 (fileName:string) =
  let doc = new XmlDocument()
  doc.CreateElement "element2" 
    |> doc.AppendChild 
    |> ignore
  doc.Save fileName    
  
let saving_to file =
  printMethod file
  save file
  file
  
[<Scenario>]
let When_saving_file() =  
  Given "file1.xml"
    |> When saving_to
    |> It should be on_disk
    |> Verify
    
[<Scenario>]
let When_comparing_files() =  
  save "file1.xml"
  save "file2.xml"
  Given "file1.xml"
    |> It should be (same_file_as "file2.xml")
    |> Verify
    
[<Scenario>]
let When_comparing_different_files() =  
  save "file1.xml"
  save2 "file2.xml"
  Given "file1.xml"
    |> It shouldn't be (same_file_as "file2.xml")
    |> Verify          