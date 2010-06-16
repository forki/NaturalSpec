module Spec.OrderProcOrderProcessingSpec

open NaturalSpec
open OrderProcessingLib

let TALISKER = "Talisker"

let filling warehouse (order:Order) =
    printMethod warehouse
    order.Fill warehouse
    order
    
let filled (order:Order) = 
    printMethod ""
    order.IsFilled

[<Scenario>]
let ``Filling removes inventory if in Stock``() =
    let order = new Order(TALISKER, 50)
    let warehouse = 
        mock<IWarehouse> "Warehouse"
            |> expectCall <@fun x -> x.HasInventory @> (TALISKER, 50) (fun _ -> true)
            |> expectCall <@fun x -> x.Remove @> (TALISKER, 50) (fun _ -> ())

    Given order
      |> When filling warehouse
      |> It should be filled
      |> Verify