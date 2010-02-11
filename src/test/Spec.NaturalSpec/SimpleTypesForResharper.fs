#light

namespace Spec.NaturalSpec

open NaturalSpec.Syntax
open NaturalSpec
open System

open NaturalSpec.Math 
open NUnit.Framework

module SimpleTypesForResharper =

  let raising x =
    printfn "raising exception %A" x
    failwith x  
      
  [<TestFixture>]
  type Test() =    
    let factorial x = 
      printMethod ""
      let rec fac_ x =
        match x with
          | 0 -> 1
          | x -> x * fac_ (x - 1)
      fac_ x
      
    // Boolean
    [<Scenario>]
    member x.When_comparing_a_false_value() =
      Given false
        |> When doing nothing
        |> It should equal false
        |> It shouldn't equal true
        |> Verify           
            
    // Integers
    [<Scenario>]
    member x.When_adding_0_to_3_it_should_not_change() =
      Given 3
        |> When adding 0
        |> It should equal 3
        |> It shouldn't equal 2
        |> Verify     
    
    [<Scenario>]
    member x.When_calculating_fac_5_it_should_equal_120() =
      Given 5
        |> When calculating factorial
        |> It should equal 120
        |> Verify    
        
    [<Scenario>]
    member x.When_calculating_fac_1_it_should_equal_1() =
      Given 1
        |> When calculating factorial
        |> It should equal 1
        |> Verify          
        
    [<Scenario>]
    member x.When_calculating_fac_0_it_should_equal_0() =
      Given 0
        |> When calculating factorial
        |> It should equal 1
        |> Verify         
      
    [<Scenario>]
    [<Fails_with_type (typeof<DivideByZeroException>)>]
    member x.When_dividing_by_zero_it_should_fail() =
      Given 10
        |> When dividing_by 0
        |> Verify          
        
    [<Scenario>]
    [<Fails_with "My error">]
    member x.When_raising_exception() =
      Given 0
        |> When raising "My error"
        |> Verify                           