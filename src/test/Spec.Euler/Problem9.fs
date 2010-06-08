module Euler.Problem9
  
open NaturalSpec

// Problem 9
// A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
// a² + b² = c²
// 
// For example, 3² + 4² = 9 + 16 = 25 = 5².
// 
// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
// Find the product abc.

let sq x = x * x

let triplets n = 
    seq { for i in 1.0..n do
            for j in i+1.0..n -> i,j }
        |> Seq.map (fun (i,j) -> i,j,sq i + sq j |> sqrt)
        |> Seq.filter (fun (_,_,k) -> k = ceil k)

let FindProduct max =
    printMethod ""
    triplets max
      |> Seq.filter (fun (a,b,c) -> a + b + c = max) 
      |> Seq.map (fun (a,b,c) -> a * b * c) 
      |> Seq.head

[<Scenario>]      
let ``Find the one Pythagorean triplet for which a + b + c = 1000``() =
    Given 1000.0
      |> When solving FindProduct
      |> It should equal 31875000.0
      |> Verify