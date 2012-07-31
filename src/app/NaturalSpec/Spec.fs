namespace NaturalSpec
open NUnit.Framework
open System

[<AttributeUsage(AttributeTargets.Method)>]
type Scenario = 
    class
        inherit TestAttribute    
        new() = { }
    end  
  
[<AttributeUsage(AttributeTargets.Method, AllowMultiple=true)>]
type Example = 
    class
        inherit TestCaseAttribute 
        new ([<ParamArray>] testParams:obj[]) = { inherit TestCaseAttribute(testParams) }
        new (given:obj,result:obj) = { inherit TestCaseAttribute(given,result) }
        new (p1:obj,p2:obj,p3:obj) = { inherit TestCaseAttribute(p1,p2,p3) }
        new (parameter:obj) =        { inherit TestCaseAttribute(parameter) }
    end

type ScenarioTemplate = Example
  
[<AttributeUsage(AttributeTargets.Method)>]
type ScenarioSource = 
    class
        inherit TestCaseSourceAttribute
        new s = {inherit TestCaseSourceAttribute(s)}
    end      
    
[<AttributeUsage(AttributeTargets.Parameter)>]
type Source = 
    class
        inherit ValueSourceAttribute
        new s = {inherit ValueSourceAttribute(s)}
    end   
    
[<AttributeUsage(AttributeTargets.Class ||| AttributeTargets.Struct ||| AttributeTargets.Method)>]
type Fails = 
    class
        inherit ExpectedExceptionAttribute
        new () = {}
        new (exceptionType:Type) = {inherit ExpectedExceptionAttribute(exceptionType)} 
    end      
  
[<AttributeUsage(AttributeTargets.Class ||| AttributeTargets.Struct ||| AttributeTargets.Method)>]
type FailsWith = 
    class
        inherit Fails
        new (s:string) as x = {inherit Fails} then x.ExpectedMessage <- s
    end
  
[<AttributeUsage(AttributeTargets.Class ||| AttributeTargets.Struct ||| AttributeTargets.Method)>]
type FailsWithType = 
    class
        inherit Fails
        new (exceptionType:Type) = {inherit Fails(exceptionType)}
    end           