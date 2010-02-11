namespace NaturalSpec
open NUnit.Framework
  
[<System.AttributeUsage(System.AttributeTargets.Method)>]
type Scenario = 
  class
    inherit TestAttribute    
    new() = { }
  end  
  
[<System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple=true)>]
type ScenarioTemplate = 
  class
    inherit TestCaseAttribute 
    new(testParams:obj[]) = { inherit TestCaseAttribute(testParams)}
    new(given:obj,result:obj) = { inherit TestCaseAttribute(given,result)}
    new(p1:obj,p2:obj,p3:obj) = { inherit TestCaseAttribute(p1,p2,p3)}
    new(parameter:obj) = { inherit TestCaseAttribute(parameter)}
  end    
  
[<System.AttributeUsage(System.AttributeTargets.Method)>]
type ScenarioSource = 
  class
    inherit TestCaseSourceAttribute
    new(s) = {inherit TestCaseSourceAttribute(s)}
  end      
    
[<System.AttributeUsage(System.AttributeTargets.Parameter)>]
type Source = 
  class
    inherit ValueSourceAttribute
    new(s) = {inherit ValueSourceAttribute(s)}
  end   
    
[<System.AttributeUsage(System.AttributeTargets.Class |||  System.AttributeTargets.Struct ||| System.AttributeTargets.Method)>]
type Fails = 
  class
    inherit ExpectedExceptionAttribute
    new() = {}
    new(exceptionType:System.Type) = {inherit ExpectedExceptionAttribute(exceptionType)} 
  end      
  
[<System.AttributeUsage(System.AttributeTargets.Class |||  System.AttributeTargets.Struct ||| System.AttributeTargets.Method)>]
type FailsWith = 
  class
    inherit Fails
    new(s:string) as x= {inherit Fails} then x.ExpectedMessage <- s
  end
  
[<System.AttributeUsage(System.AttributeTargets.Class |||  System.AttributeTargets.Struct ||| System.AttributeTargets.Method)>]
type FailsWithType = 
  class
    inherit Fails
    new(exceptionType:System.Type) = {inherit Fails(exceptionType)}
  end           