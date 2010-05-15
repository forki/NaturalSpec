[<AutoOpen>]
module StringHelper

let isPalindrome xs = 
  let list = Seq.toList xs
  List.rev list = list

let toIntList (s:string) = [for x in s -> System.Int32.Parse(x.ToString())]   

let toASCII xs =   
  let sb = new System.Text.StringBuilder()
  for x in xs do
    sb.Append(char x) |> ignore
  sb.ToString()           

let ofASCII (s:string) = [for x in s -> int x]

open NaturalSpec

let SumOfDigits n =
    printMethod ""
    sum_of_digits n

let isNullOrEmpty = System.String.IsNullOrEmpty