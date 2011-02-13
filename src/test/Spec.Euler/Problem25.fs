module Euler.Problem25
  
open NaturalSpec

// Problem 25
// The Fibonacci sequence is defined by the recurrence relation:
//
//    Fib(n) = Fib(n−1) + Fib(n−2), where Fib(1) = 1 and Fib(2) = 1.
//
// Hence the first 12 terms will be:
//
//    Fib(1) = 1
//    Fib(2) = 1
//    Fib(3) = 2
//    Fib(4) = 3
//    Fib(5) = 5
//    Fib(6) = 8
//    Fib(7) = 13
//    Fib(8) = 21
//    Fib(9) = 34
//    Fib(10) = 55
//    Fib(11) = 89
//    Fib(12) = 144
//
// The 12th term, Fib(12), is the first term to contain three digits.
//
// What is the first term in the Fibonacci sequence to contain 1000 digits?

let Fib n =
    printMethod ""
    fib (n - 2)

let FibNumberLength n =
    printMethod ""
    fibs
      |> Seq.mapi (fun i x -> i+2,x)
      |> Seq.skipWhile (fun (i,x) -> x.ToString().Length < n)
      |> Seq.head
      |> fst

[<Example(1, 1)>]
[<Example(2, 1)>]
[<Example(3, 2)>]
[<Example(4, 3)>]
[<Example(5, 5)>]
[<Example(6, 8)>]
[<Example(7, 13)>]
[<Example(8, 21)>]
[<Example(9, 34)>]
[<Example(10, 55)>]
[<Example(11, 89)>]
[<Example(12, 144)>]
let ``What is the the value of the n.th fib?`` (n,result) =
    Given n
      |> When calculating Fib
      |> It should equal (bigint result)
      |> Verify

[<Example(3, 12)>]
[<Example(1000, 4782)>]
let ``What is the first term in the Fibonacci sequence to contain n digits?`` (n,result) =
    Given n
      |> When solving FibNumberLength
      |> It should equal result
      |> Verify