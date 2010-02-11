#light
#I "tools\FAKE"
#r "FakeLib.dll"
open Fake 

// properties 
let projectName = "NaturalSpec"
  
let buildDir = @".\build\"
let docDir = @".\Doc\" 
let deployDir = @".\deploy\"
let testDir = @".\test\"
let nunitPath = @".\Tools\NUnit"
let fxCopRoot =
  let r = environVar "FXCOPROOT"
  if r <> "" && r <> null then r else 
  findFile [
    @"c:\Programme\Microsoft FxCop 1.36\"; 
    @"c:\Program Files\Microsoft FxCop 1.36\";
    @"c:\Program Files (x86)\Microsoft FxCop 1.36\"] "FxCopCmd.exe"

// files
let AppReferences  = !+ @"src\app\**\*.*proj"  |> Scan
let TestReferences = !+ @"src\test\**\*.*proj" |> Scan

let outputAssemblies =
  !+ (buildDir + @"\**\*.dll")
    ++ (buildDir + @"\**\*.exe")
    -- @"\**\*SharpZipLib*"
    -- @"\**\*SharpSvn*"
    -- "**/nunit.framework.dll"
    -- "**/Rhino.Mocks.dll"
    |> Scan
    |> Seq.map FullName

// Targets
Target "Clean" (fun () -> 
  CleanDir buildDir
  CleanDir deployDir
  CleanDir testDir
  CleanDir docDir
)

Target "BuildApp" (fun () -> 
  if not isLocalBuild then
    AssemblyInfo 
     (fun p -> 
        {p with
           CodeLanguage = FSharp;
           AssemblyVersion = buildVersion;
           AssemblyTitle = "NaturalSpec";
           AssemblyDescription = "NaturalSpec is a .NET UnitTest framework which provides automatically testable specs in natural language.";
           Guid = "62F3EDB4-1ED9-415c-A349-510DF60380B5";
           OutputFileName = @".\src\app\NaturalSpecLib\AssemblyInfo.fs"})                      

  MSBuildRelease buildDir "Build" AppReferences
    |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun () -> 
  MSBuildDebug testDir "Build" TestReferences
    |> Log "TestBuild-Output: "
)

Target "Test" (fun () ->  
  let testAssemblies = 
    !+ (testDir + @"\Spec.*.dll")     
      ++ (testDir + @"\KnightMoves.dll")
      |> Scan
      
  let output = testDir + @"TestResults.xml"
  NUnit (fun p -> 
      {p with 
         ToolPath = nunitPath; 
         DisableShadowCopy = true; 
         OutputFile = output}) 
    testAssemblies  
)

Target "GenerateDocumentation" (fun () ->
  let tool = 
    findFile [
      @"c:\Program Files (x86)\FSharp-1.9.7.8\bin\"; 
      @"c:\Program Files\FSharp-1.9.7.8\bin\";
      @"c:\Programme\FSharp-1.9.7.8\bin\"] "fshtmldoc.exe"

  let trimSlash (s:string) = s.TrimEnd('\\')
  Copy docDir [@".\HelpInput\msdn.css"]
  let commandLineBuilder =
    new System.Text.StringBuilder()
      |> appendFileNamesIfNotNull  outputAssemblies
      |> append (sprintf "--outdir\" \"%s" (docDir |> FullName |> trimSlash)) 
      |> append "--cssfile\" \"msdn.css" 
      |> append "--namespacefile\" \"namespaces.html" 
  
  trace (commandLineBuilder.ToString())
  if not (execProcess3 (fun info ->  
    info.FileName <- tool
    info.WorkingDirectory <- docDir
    info.Arguments <- commandLineBuilder.ToString()))
  then
    failwith "Documentation generation failed."
)

Target "FXCop" (fun () ->      
  FxCop 
    (fun p -> 
      {p with 
        ReportFileName = testDir + "FXCopResults.html";
        ToolPath = fxCopRoot})
    outputAssemblies
)

Target "BuildZip" (fun () ->
  let artifacts = !+ (buildDir + "\**\*.*") -- "*.zip" |> Scan
  let zipFileName = deployDir + sprintf "%s-%s.zip" projectName buildVersion
  Zip buildDir zipFileName artifacts
)

Target "ZipDocumentation" (fun () ->    
  let docFiles = 
    !+ @"Doc\**\*.*"  
      |> Scan
  let zipFileName = deployDir + sprintf "Documentation-%s.zip" buildVersion
  Zip @".\Doc\" zipFileName docFiles
)

Target "Default" DoNothing
Target "Deploy" DoNothing

// Dependencies
"BuildApp" <== ["Clean"]
"Test"     <== ["BuildApp"; "BuildTest"]
"GenerateDocumentation" <== ["BuildApp"]
"ZipDocumentation" <== ["GenerateDocumentation"]
"BuildZip" <== ["Test"]
"Deploy"   <== ["BuildZip"; "ZipDocumentation"]
"Default"  <== ["Deploy"]

// start build
run "Default"