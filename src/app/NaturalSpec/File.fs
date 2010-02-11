[<AutoOpen>]
module NaturalSpec.File
open Syntax
open System.IO

/// Tests if the file is saved
let on_disk fileName =
  printMethod ""
  let fi = new FileInfo(fileName)
  fi.Exists
  
/// Tests if the given files are identical 
let same_file_as (fileName:string) (fileName2:string) =
  printMethod fileName
  use r1 = new StreamReader(fileName)
  use r2 = new StreamReader(fileName2)
  r1.ReadToEnd() = r2.ReadToEnd()