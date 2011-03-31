#I "tools\FAKE"
#r "FakeLib.dll"
open Fake 

// properties 
let authors = ["Steffen Forkmann"]
let projectName = "NaturalSpec"
let projectDescription = "NaturalSpec is a .NET UnitTest framework which provides automatically testable specs in natural language."
  
let buildDir = @".\build\"
let docsDir = @".\Doc\" 
let deployDir = @".\deploy\"
let testDir = @".\test\"
let nugetDir = @".\nuget\" 
let nugetContentSourceDir = @".\NuGetContent\" 
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
    |> Scan
    |> Seq.map FullName

let testAssemblies = 
  !+ (testDir + @"\Spec.*.dll")
      |> Scan      

// Targets
Target? Clean <-
    fun _ -> CleanDirs [buildDir; deployDir; testDir; docsDir; nugetDir]

Target? BuildApp <-
    fun _ -> 
        if not isLocalBuild then
          AssemblyInfo 
           (fun p -> 
              {p with
                 CodeLanguage = FSharp;
                 AssemblyVersion = buildVersion;
                 AssemblyTitle = projectName;
                 AssemblyDescription = projectDescription;
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
               ToolPath = @".\tools\Docu\docu.exe"
               TemplatesPath = @".\tools\Docu\templates"
               OutputPath = docsDir })
            (!+ (buildDir @@ "NaturalSpec.dll") |> Scan)

Target? BuildZip <-
    fun _ ->
        let artifacts = !+ (buildDir + "\**\*.*") -- "*.zip" |> Scan
        let zipFileName = deployDir + sprintf "%s-%s.zip" projectName buildVersion
        Zip buildDir zipFileName artifacts

Target? ZipDocumentation <-
    fun _ ->    
        let docFiles = 
          !+ (docsDir + @"\**\*.*")
            |> Scan
        let zipFileName = deployDir + sprintf "Documentation-%s.zip" buildVersion
        Zip @".\Doc\" zipFileName docFiles

Target "BuildNuGet" (fun _ -> 
    let nugetDocsDir = nugetDir @@ "docs/"
    let nugetLibDir = nugetDir @@ "lib/"
    let nugetContentDir = nugetDir @@ "Content/"
        
    XCopy docsDir nugetDocsDir
    XCopy buildDir nugetLibDir
    XCopy nugetContentSourceDir nugetContentDir

    NuGet (fun p -> 
        {p with               
            Authors = authors
            Project = projectName
            Description = projectDescription                               
            OutputPath = nugetDir
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" })  "naturalspec.nuspec" 
)

Target? Default <- DoNothing
Target? Deploy <- DoNothing

// Dependencies
For? BuildApp <- Dependency? Clean
For? Test <- Dependency? BuildApp |> And? BuildTest
For? GenerateDocumentation <- Dependency? BuildApp
For? ZipDocumentation <- Dependency? GenerateDocumentation
For? BuildZip <- Dependency? Test
For? Deploy <- Dependency? BuildZip |> And? ZipDocumentation |> And? BuildNuGet
For? Default <- Dependency? Deploy

// start build
Run? Default