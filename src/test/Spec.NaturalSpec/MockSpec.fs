module NaturalSpec.MockSpec

let (?) (this : 'Source) (prop : string) : 'Result =
    this.GetType().GetProperty(prop).GetValue(this, null) :?> 'Result

type IFoo =
  abstract Name : string
  abstract Test: string -> string

let name (x:IFoo) = x.Name
let test s (x:IFoo) = 
    x.Test s

[<Scenario>]
let ``When getting the name of a mock``() =  
  Given (mock<IFoo> "MyMock")
    |> When getting name
    |> It should equal "Test"
    |> Verify

[<Scenario>]
let ``When calling a function on a mock``() =  
  Given (mock<IFoo> "MyMock")
    |> When calculating (test "bla")
    |> It should equal "bla"
    |> Verify