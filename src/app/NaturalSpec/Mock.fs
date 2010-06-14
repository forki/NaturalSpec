[<AutoOpen>]
module NaturalSpec.Mock

open System.Reflection
open System.Collections.Generic
open System.Reflection.Emit

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

let implementInterface<'i> (typeBuilder:TypeBuilder) (dict:FieldBuilder) =
    for m in typeof<'i>.GetMethods() do
        let attr = MethodAttributes.Public ||| MethodAttributes.HideBySig ||| MethodAttributes.Virtual
        let args = m.GetParameters()
        
        let methodImpl = 
            typeBuilder.DefineMethod(
                m.Name, 
                attr, 
                m.ReturnType, 
                args |> Array.map (fun arg -> arg.ParameterType))

        let il = methodImpl.GetILGenerator()
        il.Emit(OpCodes.Ldarg_0)

        // put method dict on stack
        il.Emit(OpCodes.Ldfld,dict)

        // put method name on stack
        il.Emit(OpCodes.Ldstr, m.Name)

        // Lookup method
        il.Emit(OpCodes.Callvirt,typeof<Dictionary<obj,obj>>.GetMethod("get_Item",[|typeof<obj>|]))
        il.Emit(OpCodes.Castclass,typeof<FSharpFunc<string,string>>)

        if args = [||] then                                               
            il.Emit(OpCodes.Ldnull)         
        else
            il.Emit(OpCodes.Ldarg_1)

        il.Emit(OpCodes.Callvirt,typeof<FSharpFunc<string,string>>.GetMethod("Invoke"))
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
    
    assemblyBuilder.Save(filename)
    
    let generatedObject = System.Activator.CreateInstance generatedType
    
    generatedType.GetField("_dict")    
      .SetValue(generatedObject, new Dictionary<obj,obj>())
              
    generatedObject :?> 'i

let registerCall methodName result mock =
    let field = mock.GetType().GetField("_dict")
    let d = field.GetValue mock :?> Dictionary<obj,obj>
    d.Add(methodName,result)

    field.SetValue(mock,d)
    mock