[<AutoOpen>]
module StringHelper

let isPalindrome xs = 
  let list = Seq.toList xs
  List.rev list = list

let toIntList (s:string) = [for x in s -> System.Int32.Parse(x.ToString())]              