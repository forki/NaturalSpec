[<AutoOpen>]
module NaturalSpec.Mock

open System.Reflection
open System.Collections.Generic
open System.Reflection.Emit

let mutable SaveAssembly = false

let createProperty<'a> (typeBuilder:TypeBuilder) propertyName =
    let t = typeof<'a>
    // Generate a private field
    let field = typeBuilder.DefineField("_" + propertyName, t, FieldAttributes.Private)

    // Generate a public property
    let property = typeBuilder.DefineProperty(propertyName, PropertyAttributes.None, t, [| t |])

    // The property set and property get methods require a special set of attributes:
    let GetSetAttr = MethodAttributes.Public |||  MethodAttributes.HideBySig ||| MethodAttributes.Virtual

    // Define the "get" accessor method for current private field.
    let currGetPropMthdBldr = typeBuilder.DefineMethod("get_" + propertyName, GetSetAttr, t, System.Type.EmptyTypes)

    // Implement Getter in ILO
    let currGetIL = currGetPropMthdBldr.GetILGenerator()
    currGetIL.Emit(OpCodes.Ldarg_0)
    currGetIL.Emit(OpCodes.Ldfld, field)
    currGetIL.Emit(OpCodes.Ret)

    // Define the "set" accessor method for current private field.
    let currSetPropMthdBldr = typeBuilder.DefineMethod("set_" + propertyName, GetSetAttr, null, [| t |])

    // Implement Setter in IL
    let currSetIL = currSetPropMthdBldr.GetILGenerator()
    currSetIL.Emit(OpCodes.Ldarg_0)
    currSetIL.Emit(OpCodes.Ldarg_1)
    currSetIL.Emit(OpCodes.Stfld, field)
    currSetIL.Emit(OpCodes.Ret)

    // Last, we must map the two methods created above to our PropertyBuilder to
    // their corresponding behaviors, "get" and "set" respectively.
    property.SetGetMethod(currGetPropMthdBldr)
    property.SetSetMethod(currSetPropMthdBldr)

let internal baseTupleType = 
    typeof<System.Tuple<int,string>>.AssemblyQualifiedName
      .Replace(typeof<int>.AssemblyQualifiedName,"{0}")
      .Replace(typeof<string>.AssemblyQualifiedName,"{1}")

let getTupleType2 (type1:System.Type) (type2:System.Type) =
    System.String.Format(baseTupleType, type1.AssemblyQualifiedName, type2.AssemblyQualifiedName)
      |> System.Type.GetType

let implementInterface<'i> (typeBuilder:TypeBuilder) (dict:FieldBuilder) =
    for m in typeof<'i>.GetMethods() do
        let attr = MethodAttributes.Public ||| MethodAttributes.HideBySig ||| MethodAttributes.Virtual
        let args = m.GetParameters() |> Array.map (fun arg -> arg.ParameterType)
        
        let methodImpl = 
            typeBuilder.DefineMethod(
                m.Name, 
                attr, 
                m.ReturnType, 
                args)

        let il = methodImpl.GetILGenerator()
     
        IlHelper.IfNotThenBlock 
            (fun il ->
                il.Emit(OpCodes.Ldarg_0)
                // put method dict on stack
                il.Emit(OpCodes.Ldfld,dict)

                // put method name on stack
                il.Emit(OpCodes.Ldstr, m.Name)

                il.Emit(OpCodes.Callvirt,typeof<Dictionary<obj,obj>>.GetMethod("ContainsKey",[|typeof<obj>|])))
            (IlHelper.throwException<System.Exception> (sprintf "Method %s was not registered." m.Name))
            il
          |> ignore

        // Lookup method
        il.Emit(OpCodes.Ldarg_0)
        // put method dict on stack
        il.Emit(OpCodes.Ldfld,dict)

        // put method name on stack
        il.Emit(OpCodes.Ldstr, m.Name)
        il.Emit(OpCodes.Callvirt,typeof<Dictionary<obj,obj>>.GetMethod("get_Item",[|typeof<obj>|]))
                 
        if args = [||] then                                               
            il.Emit(OpCodes.Ldnull)       
        else
           if args.Length = 1 then
              il.Emit(OpCodes.Ldarg_1)              
           else
              il.Emit(OpCodes.Ldarg_1)
              il.Emit(OpCodes.Ldarg_2)
              il.Emit(OpCodes.Newobj,(getTupleType2 args.[0] args.[1]).GetConstructor args)
              
        il.Emit(OpCodes.Callvirt,typeof<FSharpFunc<obj,obj>>.GetMethod "Invoke")
        if m.ReturnType = typeof<System.Void> then
            il.Emit(OpCodes.Pop)
            
        il.Emit(OpCodes.Ret)

        typeBuilder.DefineMethodOverride(methodImpl, m)

let mock<'i> name =
    let assemblyName = new AssemblyName(Name = "tmpAssembly")
    let assemblyBuilder = 
        System.AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave)

    let filename = "tmpAssembly.dll"
    let tmpModule = assemblyBuilder.DefineDynamicModule(filename,filename)

    // create a new type builder
    let typeBuilder = 
        tmpModule.DefineType(
            name, 
            TypeAttributes.Public ||| TypeAttributes.Class,
            null, // parentType
            [|typeof<'i>|])

    // Create lookip dict
    let dict = 
        typeBuilder.DefineField(
            "_dict", 
            typeof<Dictionary<obj,obj>>, 
            FieldAttributes.Public)

    implementInterface<'i> typeBuilder dict
    
    // Generate our type
    let generatedType = typeBuilder.CreateType()
    
    if SaveAssembly then
        assemblyBuilder.Save filename
    
    let generatedObject = System.Activator.CreateInstance generatedType
    
    generatedType.GetField("_dict")    
      .SetValue(generatedObject, new Dictionary<obj,obj>())
              
    generatedObject :?> 'i

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

let getMethodName (exp : Expr<'a -> 'b>) =
    match exp with
    | Lambda (_, PropertyGet (_,b,_)) -> b.GetGetMethod().Name
    | Lambda (_, Lambda (_, Call (_, b, _))) -> b.Name
    | Lambda (_, Lambda (_, Let (_, _, Let (_, _, Call (_, b, _))))) -> b.Name
    | Lambda (_, Let (_, Call (_, _, Lambda (_, Call (_,c,_))::_), Lambda (_, Call (_, b, _)))) 
        when b.Name = "AddHandler" -> c.Name
    | _ -> failwithf "Unknown pattern %A" exp

let registerCall (exp : Expr<'a -> 'b>) (resultF: 'c -> 'd) (mock:'a) =
    let field = mock.GetType().GetField "_dict"
    let d = field.GetValue mock :?> Dictionary<obj,obj>
    
    d.Add(getMethodName exp,resultF)

    field.SetValue(mock,d)
    mock

let expectAnyCall exp resultF mock =
    let methodName = getMethodName exp
    let wasCalled = ref false
    let called x = 
        wasCalled := true
        resultF x

    let expectation() =
        if !wasCalled then null
        else new System.Exception(sprintf "Method %s was not called on %A." methodName mock) 

    Expectations.add(Expectations.getScenarioName(),expectation)
    registerCall exp called mock


let registerProperty (exp : Expr<'a -> 'b>)  (resultF:unit -> 'b) (mock:'a) = expectAnyCall exp resultF mock
let register (exp : Expr<'a -> ('b -> 'c)>)  (resultF:('b -> 'c)) (mock:'a) = expectAnyCall exp resultF mock

let expectCall (exp : Expr<'a -> ('b -> 'c)>)  parameter (resultF:('b -> 'c)) (mock:'a) =
    let field = mock.GetType().GetField "_dict"
    let d = field.GetValue mock :?> Dictionary<obj,obj>
    let methodName = getMethodName exp
    let wasCalled = ref false

    Expectations.add(
        Expectations.getScenarioName(),
        (fun _ ->
            if !wasCalled then null
            else new System.Exception(sprintf "Method %s was not called with %A on %A." methodName parameter mock)))

    match d.TryGetValue methodName with
    | true,m ->
        let m1 = m :?> ('b -> 'c)
        let called x = 
            if x = parameter then
                wasCalled := true
                resultF x
            else m1 x

        d.[methodName] <- called
    | _ -> 
        let called x = 
            if x = parameter then
                wasCalled := true
                resultF x
            else failwithf "No result given for %s (%A) on %A" methodName parameter mock

        d.Add(methodName,called)

    field.SetValue(mock,d)
    mock