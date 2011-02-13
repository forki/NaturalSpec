module Spec.OrderProcOrderProcessingSpec

open System
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
let ``Filling order removes items from inventory if in Stock``() =
    let order = new Order(TALISKER, 50)
    let warehouse = 
        mock<IWarehouse> "Warehouse"
            |> setup <@fun x -> x.HasInventory @> (fun _ -> true)
            |> setup <@fun x -> x.Remove @> (fun _ -> ())

    Given order
      |> When filling warehouse
      |> It should be filled
      |> Whereas warehouse
      |> Called <@fun x -> x.HasInventory @> (TALISKER, 50)
      |> Called <@fun x -> x.Remove @> (TALISKER, 50)
      |> Verify

[<Scenario>]
let ``Filling order doesn't remove items from inventory if not enough in Stock``() =
    let order = new Order(TALISKER, 50)
    let warehouse = 
        mock<IWarehouse> "Warehouse"
            |> setup <@fun x -> x.HasInventory @> (fun _ -> false)

    Given order
      |> When filling warehouse
      |> It shouldn't be filled
      |> Whereas warehouse
      |> Called <@fun x -> x.HasInventory @> (TALISKER, 50)
      |> Verify