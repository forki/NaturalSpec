[<AutoOpen>]
module NaturalSpec.Math
open Syntax
open System

let rand = new Random()

/// generates a list of n random floats
let list_of_random_floats n = List.init n (fun _ -> rand.NextDouble())

/// generates a list of n random ints
let list_of_random_ints n = List.init n (fun _ -> rand.Next())


/// Adds x and y
let adding x y =  
    printMethod x
    x + y
  
/// Divides x and y
let dividing_by y x=       
    sprintf "dividing by %A" y |> Utils.toSpec
    x / y
  
/// Multiplies x and y
let multiply_with y x=       
    sprintf "multiply with %A" y |> Utils.toSpec
    x * y    

/// Calculate f(x)
let calculating f x =  
    printMethod ""
    f x