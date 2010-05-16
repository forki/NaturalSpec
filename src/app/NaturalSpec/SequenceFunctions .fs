[<AutoOpen>]
module NaturalSpec.SequenceFunctions 

open Syntax
open System
open Utils
open NUnit.Framework
  
// Methods

/// Remove an element from the sequence
let Removing x seq =
  printMethod x
  seq |> Seq.filter ((<>) x)
      
// Assertions

/// Test if the element is in the sequence
let contain x (seq:'a seq) =
  printMethod x
  IsTrue,true,Seq.exists ((=) x) seq,seq
  
/// Test if the element is in the sequence
let duplicates (seq:'a seq) =
  printMethod ""
  let dict = new System.Collections.Generic.HashSet<'a>()
  seq |> Seq.fold (fun d i -> d || (not (dict.Add i))) false
  
/// Test if sequence has length n
let length n (seq:'a seq) =
  printMethod n
  Assert.AreEqual(n, seq |> Seq.length)
  true
  
// Constants

/// Test if the sequence is empty
let empty seq =
    printMethod ""
    Seq.isEmpty seq

/// Applies the sorting function f
let sorting_with f =
    printMethod ""
    f    

/// Test if the sequence is sorted
let sorted seq =
    printMethod ""
    if Seq.isEmpty seq then true else
    let l = ref (Seq.head seq)
    Seq.forall (fun e -> if e >= !l then l := e; true else false) seq

/// Test if the sequence contains all elements from sequence x
let contain_all_elements_from x y =
    printMethod x
    let found = x |> Seq.forall (fun element -> Seq.exists ((=) element) y)
    IsTrue,true,found,y

/// Test if the sequence contains no other elements than sequence x
let contain_no_other_elements_than x y =
    printMethod x
    let found = y |> Seq.forall (fun element -> Seq.exists ((=) element) x)
    IsTrue,true,found,y