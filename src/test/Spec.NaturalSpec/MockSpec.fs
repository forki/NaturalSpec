module NaturalSpec.MockSpec

let (?) (this : 'Source) (prop : string) : 'Result =
    this.GetType().GetProperty(prop).GetValue(this, null) :?> 'Result

type IFoo =
  abstract Name : string
  abstract Test: string -> string
  abstract Add: int*int -> int

let name (x:IFoo) = 
    printMethod ""
    x.Name

let test s (x:IFoo) = 
    printMethod s
    x.Test s

let add a b (x:IFoo) = 
    printMethod (sprintf "%d + %d" a b)
    let result = x.Add(a,b)
    result

[<Scenario>]
let ``When getting the name of a mock``() =  
    let m =
        mock<IFoo> "MyMock"
          |> registerCall <@fun x -> x.Name @> (fun _ -> "MyName")

    Given m
      |> When getting name
      |> It should equal "MyName"
      |> Verify

[<Scenario>]
let ``When calling a function on a mock``() =    
    let m =
        mock<IFoo> "MyMock"
          |> register <@fun x -> x.Test @> (fun x -> if x = "bla" then "blub" else x)

    Given m
      |> When calculating (test "bla")
      |> It should equal "blub"
      |> Verify

[<Scenario>]
let ``When calling add on a mock``() =  
    let m =
        mock<IFoo> "MyMock"
          |> register <@fun x -> x.Add @> (fun (x,y) -> x + y)

    Given m
      |> When calculating (add 4 5)
      |> It should equal 9
      |> Verify