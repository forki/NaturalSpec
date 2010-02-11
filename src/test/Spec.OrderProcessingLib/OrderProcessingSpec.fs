module Spec.OrderProcOrderProcessingSpec

open NaturalSpec
open OrderProcessingLib

let shipping order (x:OrderProcessing) =
  printMethod order
  x.Ship(order) |> ignore
  x

let EmailService = mock<IEmailService> "EMailService"
let Order = mock<IOrder> "Order"
let OrderProcessing = OrderProcessing EmailService

[<Scenario>]
let When_shipping_order() =
  Given OrderProcessing
    |> Expect EmailService.Send
    |> Expect Order.Ship
    |> When shipping Order
    |> Verify
