module NaturalSpec.IlHelper

open System.Reflection
open System.Collections.Generic
open System.Reflection.Emit


let throwException<'a> (message:string) (il:ILGenerator) =
    il.Emit(OpCodes.Ldstr, message)
    il.Emit(OpCodes.Newobj,typeof<'a>.GetConstructor [|typeof<string>|])
    il.Emit(OpCodes.Throw)
    il

let private ifThenBlock not (conditionF:ILGenerator -> 'a) (thenF:ILGenerator -> 'b) (il:ILGenerator) =
    let local = il.DeclareLocal(typeof<bool>)
    conditionF il |> ignore

    il.Emit(OpCodes.Stloc, local)
    il.Emit(OpCodes.Ldloc, local)

    let skipTo = il.DefineLabel()

    if not then
        il.Emit(OpCodes.Brtrue_S,skipTo)
    else
        il.Emit(OpCodes.Brfalse_S,skipTo)

    thenF il |> ignore
        
    il.MarkLabel skipTo
    il

let IfThenBlock conditionF = ifThenBlock false conditionF
let IfNotThenBlock conditionF = ifThenBlock true conditionF

let IfThenElseBlock conditionF thenF elseF (il:ILGenerator) =
    let endLabel = il.DefineLabel()     
    let il = ifThenBlock false conditionF thenF il
    
    elseF il |> ignore

    il.Emit(OpCodes.Br_S,endLabel)

    il.MarkLabel endLabel

    il