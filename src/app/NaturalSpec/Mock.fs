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
    let GetSetAttr = MethodAttributes.Public |||  MethodAttributes.HideBySig

    // Define the "get" accessor method for current private field.
    let currGetPropMthdBldr = typeBuilder.DefineMethod("get_value", GetSetAttr, t, System.Type.EmptyTypes)

    // Implement Getter in ILO
    let currGetIL = currGetPropMthdBldr.GetILGenerator()
    currGetIL.Emit(OpCodes.Ldarg_0)
    currGetIL.Emit(OpCodes.Ldfld, field)
    currGetIL.Emit(OpCodes.Ret)

    // Define the "set" accessor method for current private field.
    let currSetPropMthdBldr = typeBuilder.DefineMethod("set_value", GetSetAttr, null, [| t |])

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

let mock name =
    let assemblyName = new AssemblyName(Name = "tmpAssembly")
    let assemblyBuilder = 
            System.Threading.Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run)

    let tmpModule = assemblyBuilder.DefineDynamicModule "tmpModule"

    // create a new type builder
    let typeBuilder = tmpModule.DefineType(name, TypeAttributes.Public ||| TypeAttributes.Class)

    createProperty<string> typeBuilder "Name"

    // Generate our type
    let generatedType = typeBuilder.CreateType()

    // Now we have our type. Let's create an instance from it:
    let generatedObject = System.Activator.CreateInstance(generatedType)

    // Fill properties    
    generatedType.GetProperty("Name").SetValue(generatedObject, name, null)

    generatedObject