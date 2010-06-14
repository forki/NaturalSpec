module NaturalSpec.MockSpec

let (?) (this : 'Source) (prop : string) : 'Result =
    this.GetType().GetProperty(prop).GetValue(this, null) :?> 'Result

type IFoo =
  abstract Name : string
  abstract Test: string -> string

let name (x:IFoo) = 
    printMethod ""
    x.Name

let test s (x:IFoo) = 
    printMethod s
    x.Test s

[<Scenario>]
let ``When getting the name of a mock``() =  
    let m =
        mock<IFoo> "MyMock"
          |> registerCall "get_Name" (fun (x:string) -> "MyName")

    Given m
      |> When getting name
      |> It should equal "MyName"
      |> Verify

[<Scenario>]
let ``When calling a function on a mock``() =  
    let m =
        mock<IFoo> "MyMock"
          |> registerCall "Test" (fun (x:string) -> if x = "bla" then "blub" else x)

    Given m
      |> When calculating (test "bla")
      |> It should equal "blub"
      |> Verify