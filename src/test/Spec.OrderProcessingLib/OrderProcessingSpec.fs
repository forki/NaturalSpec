module Spec.OrderProcessingSpec

open System
open NaturalSpec
open OrderProcessingLib

let TALISKER = "Talisker"

let filling_at warehouse (order:Order) =
    printMethod warehouse
    order.Fill warehouse
    order
    
let filled (order:Order) = 
    printMethod ""
    order.IsFilled

[<Scenario>]
let ``Filling order removes items from inventory if in stock``() =
    let order = new Order(TALISKER, 50)
    let warehouse = 
        mock<IWarehouse> "Warehouse"
            |> setup <@fun x -> x.HasInventory @> AlwaysTrue   // fun _ -> true
            |> setup <@fun x -> x.Remove @> AlwaysUnit

    Given order
      |> When filling_at warehouse
      |> It should be filled
      |> Whereas warehouse
      |> Called <@fun x -> x.HasInventory @> (TALISKER, 50)
      |> Called <@fun x -> x.Remove @> (TALISKER, 50)
      |> Verify

[<Scenario>]
let ``Filling order doesn't remove items from inventory if not enough in stock``() =
    let order = new Order(TALISKER, 50)
    let warehouse = 
        mock<IWarehouse> "Warehouse"
            |> setup <@fun x -> x.HasInventory @> AlwaysFalse

    Given order
      |> When filling_at warehouse
      |> It shouldn't be filled
      |> Whereas warehouse
      |> Called <@fun x -> x.HasInventory @> (TALISKER, 50)
      |> Verify