[<AutoOpen>]
module NaturalSpec.Syntax

open NUnit.Framework
open System.Diagnostics
open Utils
open TimeMeasurement

/// Get the method name      
let methodName n (x:obj) = 
    let methodName = (new StackTrace()).GetFrame(n).GetMethod().Name.Replace("_"," ")    
    match x with
    | :? string as s -> sprintf "%s %s" methodName s
    | _              -> sprintf "%s %s" methodName (prepareOutput x)
     
/// Prints the method name and the given parameter to the spec output           
let printMethod (x:obj) = 
    methodName 2 x
      |> toSpec


/// Calls  
let calls = new System.Collections.Generic.HashSet<string * string>()

/// Register a expected call
let calling methodName param = calls.Add(methodName, param.ToString()) |> ignore

/// Inits a scenario    
let initScenario() =  
    stopWatch.Reset()
    stopWatch.Start()
    printScenario()

/// Sets a test context up - same as "As"  
/// Represents the Arrange phase of "Arrange"-"Act"-"Assert"      
let Given f = 
    initScenario()
        
    sprintf "  - Given %s" (prepareOutput f) |> toSpec
    f

/// Sets a test context up - same as "Given"  
let As f =  
    initScenario()
        
    sprintf "  - As %s" (prepareOutput f)  |> toSpec
    f

/// Acts on the given test context
/// Represents the Act phase of "Arrange"-"Act"-"Assert"      
let When f = 
    toSpec  "\n     - When "
    f
    
/// Fluid helper - prints "doing "
let doing f =
    toSpec "doing "
    f        

/// Fluid helper - prints "nothing "  
let nothing f =
    toSpec "nothing "
    f

/// Fluid helper - prints "solving"
let solving f =
    printMethod ""
    f


/// Fluid helper - prints "getting"
let getting f =
    printMethod ""
    f    

/// Tests a condition on the manipulated test context
/// Represents the Assert phase of "Arrange"-"Act"-"Assert"         
let It f = 
    toSpec "\n      => It "
    f
  
/// Tests a condition on the given value
/// Represents the Assert phase of "Arrange"-"Act"-"Assert"    
let Whereas v f = 
    toSpec <| sprintf "\n      => Whereas %A " v
    v  

/// Tests for equality
let checkEquality (expected:'a) (value:'a) pipe = Equality,expected,value,pipe
  
/// Fluid helper - prints "equal "
/// Tests for equality
/// Use it as in "|> It should equal x"
let equal (expected:'a) (value:'a) = 
    sprintf "equal %s" (prepareOutput expected) |> toSpec
    checkEquality expected value value    
  
/// Fluid helper - prints "be "
/// Tests for boolean condition
/// Use it as in "|> It should be true"    
let be (f:'a -> bool) value = 
    toSpec "be "
    IsTrue,true,f value,value     
  
/// Fluid helper - prints "have "
/// Tests for boolean condition
/// Use it as in "|> It should be true"    
let have (f:'a-> bool) value = 
    toSpec "have "
    IsTrue,true,f value,value     

/// Fluid helper - prints "called "
/// Tests for boolean condition
/// Use it as in "|> It should have called"  
let called methodName param _ = 
    let p = param.ToString()
    toSpec <| sprintf "called %s with %s" (prepareOutput methodName) p
    calls.Contains(methodName,p)
        
/// Fluid helper - prints "should "
/// Tests if the given observation hold
/// Use it as in "|> It should equal 5"              
let should f x y =
    toSpec "should "
    f x y |> check      

/// Fluid helper - prints "should not "
/// Tests if the given observation does not hold
/// Use it as in "|> It shouldn't equal 5"
let shouldn't f x y =
    toSpec "should not "
    f x y |> not' |> check 

/// generates TestCaseData object
let testData x y z = new TestCaseData(x, [|box y; box z|])

type TestCaseParam = 
| SingleParam of obj
| DoubleParam of obj*obj
| TripleParam of obj*obj*obj
| MultiParam of obj[]
 
let singleParam a = SingleParam(box a)
let doubleParam a b = DoubleParam(box a,box b)
let tripleParam a b c = TripleParam(box a,box b,box c)

/// Generates a testcase
let TestWith p = 
  match p with
  | SingleParam a       -> [new TestCaseData(a)]
  | DoubleParam (a,b)   -> [new TestCaseData(a, b)]
  | TripleParam (a,b,c) -> [new TestCaseData(a, b, c)]
  | MultiParam  y       -> [new TestCaseData(y)]
      
/// Generates from a list
let TestWithList (seq: 'a seq) = 
    seq
      |> Seq.map (fun e -> new TestCaseData(box e))

/// Adds a test case to the list of testcases
let And p list = TestWith p @ list  

/// Generates testdata from n-times application of the given function
let GenerateTestData f n =
   {1..n} 
     |> Seq.map (TestWith << f)   

/// Adds an ExpectedException to the testcase
let ShouldFailWith (exceptionType:System.Type) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "No TestData given"
    | x::rest -> (x.Throws exceptionType)::rest  
    
/// Adds an Description to the testcase
let Named (name:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "No TestData given"
    | x::rest -> (x.SetName name)::rest        
      
/// Verifies a scenario   
let Verify x =
    prepareOutput x
      |> sprintf "\r\n  ==> Result is: %s" 
      |> toSpec
    toSpec "\r\n  ==> OK"    
    printElapsed()
    toSpec "\r\n"