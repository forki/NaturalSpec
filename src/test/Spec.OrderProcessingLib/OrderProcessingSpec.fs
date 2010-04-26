module Spec.OrderProcOrderProcessingSpec

open NaturalSpec
open OrderProcessingLib

let shipping order (x:OrderProcessing) =
  printMethod order
  x.Ship order |> ignore
  x

let mockEmailService called = 
    {new IEmailService with
        member x.Send() = called() }

let mockOrder called = 
    {new IOrder with
        member x.Ship() = called() }

[<Scenario>]
let When_shipping_order() =
  let serviceCalled = ref false
  let orderCalled = ref false
  let OrderProcessing = new OrderProcessing(mockEmailService (fun () -> serviceCalled := true))
  let Order = mockOrder (fun () -> orderCalled := true)
  Given OrderProcessing
    |> When shipping Order
    |> Verify