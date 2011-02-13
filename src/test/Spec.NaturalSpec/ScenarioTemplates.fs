module NaturalSpec.Examples

/// Tail recursive version
let factorial x =
    let rec tailRecursiveFactorial x acc =
        match x with
        | y when y = 0 -> acc
        | _ -> tailRecursiveFactorial (x-1) (acc*x)           

    tailRecursiveFactorial x 1
  
// with Example Attribute
[<Example(1, 1)>]  
[<Example(2, 2)>]
[<Example(5, 120)>]
[<Example(10, 3628800)>]
let ``Calculating factorial for n`` (x,result) =
  Given x
    |> When calculating factorial
    |> It should equal result
    |> Verify 
    
// with ScenarioTemplate Attribute
[<ScenarioTemplate(1, 1)>]  
[<ScenarioTemplate(2, 2)>]
[<ScenarioTemplate(5, 120)>]
[<ScenarioTemplate(10, 3628800)>]
let ``Calculating factorial for n with templates`` (x,result) =
  Given x
    |> When calculating factorial
    |> It should equal result
    |> Verify 
          
// predefined scenario
let factorialScenario x result =
  Given x
    |> When calculating factorial
    |> It should equal result
    
[<Scenario>]
let ``Factorial of 1 = 1``() =
  factorialScenario 1 1 
    |> Verify   
  
[<Scenario>]
let ``Factorial of 2 = 2``() =
  factorialScenario 2 2 
    |> Verify
  
[<Scenario>]
let ``Factorial of 5 = 120``() =
  factorialScenario 5 120 
    |> Verify
  
[<Scenario>]
let ``Factorial of 10 = 3628800 ``() =
  factorialScenario 10 3628800 
    |> Verify     

/// with a scenario source      
let MyTestCases =
  TestWith (tripleParam 12 3 4)
    |> And (tripleParam 12 4 3)
    |> And (tripleParam 12 6 2)
    |> And (tripleParam 0 0 0) 
      |> ShouldFailWith (typeof<System.DivideByZeroException>)
    |> And (tripleParam 1200 40 30)        

[<ScenarioSource "MyTestCases">]
let ``Dividing b by a`` a b result =
 Given a 
   |> When dividing_by b
   |> It should equal result
   |> Verify       
   
/// with a scenario source      
let MyTestCases2 = TestWithList [0..5]

[<ScenarioSource "MyTestCases2">]
let ``Multiply 0 with a`` a =
 Given 0 
   |> When multiply_with a
   |> It should equal 0
   |> Verify     
   
   
/// with a scenario source      
let MyTestCases3 =
  let rand = new System.Random()    
  let f _ = 
    let a = rand.Next()
    let b = rand.Next(1,100) 
    (a,b,(a/b))
    
  Array.init 10 f
  

[<Scenario>]
let ``Dividing ints`` ([<Source "MyTestCases3">] p) =
   let a,b,result = unbox p
   Given a 
     |> When dividing_by b
     |> It should equal result
     |> Verify