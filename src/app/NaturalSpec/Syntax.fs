[<AutoOpen>]
module NaturalSpec.Syntax

open NUnit.Framework
open System.Diagnostics
open Rhino.Mocks
open Utils
open TimeMeasurement
 
/// Create a named Mock object
let mock<'a>(name:string) = 
  let m = mocks.StrictMock<'a>(null)
  mockNameDict.Add(m.ToString(),name)
  m 
     
/// Prints the method name and the given parameter to the spec output           
let printMethod (x:obj) = 
  let methodName = (new StackTrace()).GetFrame(1).GetMethod().Name.Replace("_"," ")    
  match x with
    | :? string as s     -> sprintf "%s %s" methodName s |> toSpec
    | _                  ->          
       sprintf "%s %s" methodName (getMockName x) |> toSpec
  
/// Inits a scenario    
let initScenario() =  
  stopWatch.Reset()
  stopWatch.Start()
  refreshMocks()
  printScenario()

/// Registers a mock        
let Mock (f:'a -> 'b) (p:'a) returns context =
  let e = Expect.Call<'b>( f(p) )
  sprintf "\n     - With mocking" |> toSpec
  e.Return returns |> ignore
  context
  
/// Registers a mock call        
let Expect f context =
  let e = Expect.Call<unit>( f() )
  sprintf "\n     - With mocking" |> toSpec
  context    

/// Sets a test context up - same as "As"  
/// Represents the Arrange phase of "Arrange"-"Act"-"Assert"      
let Given f = 
  initScenario()
        
  sprintf "  - Given %s" (getMockName f) |> toSpec
  f

/// Sets a test context up - same as "Given"  
let As f =  
  initScenario()
        
  sprintf "  - As %s" (getMockName f)  |> toSpec
  f

/// Acts on the given test context
/// Represents the Act phase of "Arrange"-"Act"-"Assert"      
let When f = 
  mocks.ReplayAll()
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
let checkEquality (expected:'a) (value:'a) pipe = 
  Equality,expected,value,pipe
  
/// Fluid helper - prints "equal "
/// Tests for equality
/// Use it as in "|> It should equal x"
let equal (expected:'a) (value:'a) = 
  sprintf "equal %s" (getMockName expected) |> toSpec
  checkEquality expected value value    
  
/// Fluid helper - prints "be "
/// Tests for boolean condition
/// Use it as in "|> It should be true"    
let be f value = 
  toSpec "be "
  IsTrue,true,(f value:bool),value     
  
/// Fluid helper - prints "have "
/// Tests for boolean condition
/// Use it as in "|> It should be true"    
let have f value = 
  toSpec "have "
  IsTrue,true,(f value:bool),value     
        
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
let TestWith (p:TestCaseParam) = 
  match p with
  | SingleParam a       -> [new TestCaseData(a)]
  | DoubleParam (a,b)   -> [new TestCaseData(a, b)]
  | TripleParam (a,b,c) -> [new TestCaseData(a, b, c)]
  | MultiParam  y       -> [new TestCaseData(y)]
      
/// Generates from a list
let TestWithList (list: 'a seq) = 
  list |> Seq.fold (fun r e -> new TestCaseData(box e)::r) []    

/// Adds a test case to the list of testcases
let And (p:TestCaseParam) list = TestWith p @ list  

/// Generates testdata from n-times application of the given function
let GenerateTestData f n =
   {2..n} 
     |> Seq.fold 
         (fun acc i -> And (f i) acc)
         (TestWith (f 1))
    

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
  x |> ignore
  try
    mocks.VerifyAll()
    
    sprintf "\n  ==> Result is: %s" (prepareOutput x) |> toSpec
    toSpec "\n  ==> OK"
    printElapsed()
  with 
    | ex -> 
        sprintf "\n  ==> Expected mocked call missing" |> toSpec
        printElapsed()
        raise ex
       