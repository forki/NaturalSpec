module Spec.OrderProcOrderProcessingSpec

open NaturalSpec
open OrderProcessingLib

let shipping order (x:OrderProcessing) =
    printMethod order
    x.Ship order |> ignore
    x

let mockEmailService = 
    {new IEmailService with
        member x.Send() = calling "EmailService.Send" "" }

let order = 
    {new IOrder with
        member x.Ship() = calling "Order.Ship" "" }

[<Scenario>]
let ``When shipping order``() =
  let OrderProcessing = new OrderProcessing(mockEmailService)
  
  Given OrderProcessing
    |> When shipping order
    |> It should have (called "EmailService.Send" "")
    |> It should have (called "Order.Ship" "")
    |> Verify