module NaturalSpec.Fibonacci

//let rec fib = function
//| 0 -> 0
//| 1 -> 1
//| n -> fib (n-1) + fib (n-2)

let q1 = 1I,1I,1I,0I

let mult (a11,a12,a21,a22) (b11,b12,b21,b22) =
    a11 * b11 + a12 * b21,
    a11 * b12 + a12 * b22,
    a21 * b11 + a22 * b21,
    a21 * b12 + a22 * b22

let sq x = mult x x

let rec pow x n =
    match n with
    | 1 -> x
    | z when z % 2 = 0 -> pow x (n/2) |> sq
    | _ -> pow x ((n-1)/2) |> sq |> mult x

let fib n =
    let _,x,_,_ = pow q1 n
    x

let Fibonacci n = 
    printMethod n  // prints the method name via reflection
    fib n          // Abstraction of SUT
              
[<Scenario>]
let ``Fibonacci of 1 = 1``() =          // Scenario-Name
    Given 1                             // Arrange
    |> When calculating Fibonacci       // Act
    |> It should equal 1I               // Assert
    |> Verify   

  
// shorter with scenario templates
[<Example(1,1)>]
[<Example(2,1)>]
[<Example(3,2)>]
[<Example(4,3)>]
[<Example(5,5)>]
[<Example(46,1836311903)>]
let ``Fibonacci of n = m``(n,m:int) =
    Given n
    |> When calculating Fibonacci
    |> It should equal (bigint m)
    |> Verify


/// with a scenario source      
let exampleFibs =
    TestWith (1  ==> 1I)
      |> And (2  ==> 1I)
      |> And (3  ==> 2I)
      |> And (4  ==> 3I)
      |> And (5  ==> 5I)
      |> And (46 ==> 1836311903I)

[<ScenarioSource "exampleFibs">]
let ``Fibonacci of n should equal m``  n m =
    Given n
    |> When calculating Fibonacci
    |> It should equal m
    |> Verify   
