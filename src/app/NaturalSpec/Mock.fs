[<AutoOpen>]
module NaturalSpec.Mock

open System.Reflection
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

let implementInterface<'i> (typeBuilder:TypeBuilder) =
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
        if args = [||] then                    
            il.Emit(OpCodes.Ldarg_0)
            il.Emit(OpCodes.Ldstr, "Test")
            il.Emit(OpCodes.Ret)
        else
            il.Emit(OpCodes.Ldarg_0)
            il.Emit(OpCodes.Ldarg_1)
            il.Emit(OpCodes.Ret)

        typeBuilder.DefineMethodOverride(methodImpl, m)

let mock<'i> name =
    let assemblyName = new AssemblyName(Name = "tmpAssembly")
    let assemblyBuilder = 
            System.Threading.Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run)

    let tmpModule = assemblyBuilder.DefineDynamicModule "tmpModule"

    // create a new type builder
    let typeBuilder = 
        tmpModule.DefineType(
            name, 
            TypeAttributes.Public ||| TypeAttributes.Class,
            null, // parentType
            [|typeof<'i>|])

    implementInterface<'i> typeBuilder

    // Generate our type
    let generatedType = typeBuilder.CreateType()

    // Now we have our type. Let's create an instance from it:
    let generatedObject = System.Activator.CreateInstance(generatedType)


    // Fill properties    
  //  generatedType.GetProperty("Name").SetValue(generatedObject, name, null)
    
    generatedObject :?> 'i