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

let testAssemblies = 
  !+ (testDir + @"\Spec.*.dll")     
    ++ (testDir + @"\KnightMoves.dll")
      |> Scan      

// Targets
Target? Clean <-
    fun _ -> CleanDirs [buildDir; deployDir; testDir; docDir]

Target? BuildApp <-
    fun _ -> 
        if not isLocalBuild then
          AssemblyInfo 
           (fun p -> 
              {p with
                 CodeLanguage = FSharp;
                 AssemblyVersion = buildVersion;
                 AssemblyTitle = "NaturalSpec";
                 AssemblyDescription = "NaturalSpec is a .NET UnitTest framework which provides automatically testable specs in natural language.";
                 Guid = "62F3EDB4-1ED9-415c-A349-510DF60380B5";
                 OutputFileName = @".\src\app\NaturalSpec\AssemblyInfo.fs"})                      

        MSBuildRelease buildDir "Build" AppReferences
          |> Log "AppBuild-Output: "

Target? BuildTest <-
    fun _ -> 
        MSBuildDebug testDir "Build" TestReferences
          |> Log "TestBuild-Output: "

Target? Test <-
    fun _ ->           
        NUnit (fun p -> 
          {p with 
             ToolPath = nunitPath; 
             DisableShadowCopy = true; 
             OutputFile = testDir + @"TestResults.xml"}) 
          testAssemblies  

Target? GenerateDocumentation <-
    fun _ ->
        Docu (fun p ->
            {p with
               ToolPath = @".\tools\FAKE\docu.exe"
               TemplatesPath = @".\tools\FAKE\templates"
               OutputPath = docDir })
            (buildDir + "NaturalSpec.dll")

Target? BuildZip <-
    fun _ ->
        let artifacts = !+ (buildDir + "\**\*.*") -- "*.zip" |> Scan
        let zipFileName = deployDir + sprintf "%s-%s.zip" projectName buildVersion
        Zip buildDir zipFileName artifacts

Target? ZipDocumentation <-
    fun _ ->    
        let docFiles = 
          !+ (docDir + @"\**\*.*")
            |> Scan
        let zipFileName = deployDir + sprintf "Documentation-%s.zip" buildVersion
        Zip @".\Doc\" zipFileName docFiles

Target? Default <- DoNothing
Target? Deploy <- DoNothing

// Dependencies
For? BuildApp <- Dependency? Clean
For? Test <- Dependency? BuildApp |> And? BuildTest
For? GenerateDocumentation <- Dependency? BuildApp
For? ZipDocumentation <- Dependency? GenerateDocumentation
For? BuildZip <- Dependency? Test
For? Deploy <- Dependency? BuildZip |> And? ZipDocumentation
For? Default <- Dependency? Deploy

// start build
Run? Default