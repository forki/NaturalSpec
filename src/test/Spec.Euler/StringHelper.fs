[<AutoOpen>]
module StringHelper

let isPalindrome xs = 
  let list = Seq.toList xs
  List.rev list = list