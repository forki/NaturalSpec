module Problem2 
  
open NaturalSpec

let fibs = 
    Seq.unfold (fun (a,b) -> 
      let sum = a + b
      Some(sum, (b, sum))) 
      (0,1)

let SumOfEvenValuedTermsInTheFibonacciSequence max =
    printMethod ""
    fibs
      |> Seq.filter (fun x -> x % 2 = 0)
      |> Seq.takeWhile (fun x -> x <= max)
      |> Seq.sum
    
[<Scenario>]      
let Problem2_WhenFindingSumOfEvenValuedTermsInTheFibonacciSequenceUpTo89 () =     
    Given 89
      |> When solving SumOfEvenValuedTermsInTheFibonacciSequence
      |> It should equal 44
      |> Verify
    
[<Scenario>]
let Problem2_WhenFindingSumOfEvenValuedTermsInTheFibonacciSequenceBelow4Million () =   
    Given 4000000
      |> When solving SumOfEvenValuedTermsInTheFibonacciSequence
      |> It should equal 4613732
      |> Verify