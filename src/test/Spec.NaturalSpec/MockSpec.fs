module NaturalSpec.MockSpec

let (?) (this : 'Source) (prop : string) : 'Result =
    this.GetType().GetProperty(prop).GetValue(this, null) :?> 'Result

let name x = x?Name

[<Scenario>]
let ``Creating a mock``() =  
  Given (mock "MyMock")
    |> When getting name
    |> It should equal "MyMock"
    |> Verify