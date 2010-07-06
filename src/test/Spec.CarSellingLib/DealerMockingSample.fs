// 1. define module
module Spec.CarSellingLib.DealerMockingSample

// 2. open NaturalSpec-Namespace
open NaturalSpec
        
// 3. open project namespace
open CarSellingLib

// define reusable values
let DreamCar = new Car(CarType.BMW, 200)
let LameCar = new Car(CarType.Fiat, 45)

let getCarByPrice price = if price < 20000 then LameCar else DreamCar

// 4. create a method in BDD-style
let selling_a_car_for amount (dealer:IDealer) =
    printMethod amount
    dealer.SellCar amount

// 5. create a scenario      
[<Scenario>]
let ``When selling the DreamCar for 40000``() =     
    let bert = 
        mock<IDealer> "Bert"
          |> setup <@fun x -> x.SellCar @> getCarByPrice

    As bert
      |> When selling_a_car_for 40000
      |> It should equal DreamCar
      |> Whereas bert
      |> Called <@fun x -> x.SellCar @> 40000
      |> Verify
    
     
[<Scenario>]
let ``When selling the Lamecar for 19000``() = 
    let bert =
        mock<IDealer> "Bert"
          |> setup <@fun x -> x.SellCar @> getCarByPrice

    As bert
      |> When selling_a_car_for 19000
      |> It should equal LameCar
      |> Whereas bert
      |> Called <@fun x -> x.SellCar @> 19000
      |> Verify

[<Scenario>]
[<FailsWith "Method SellCar was not called with 19000 on Bert.">]
let ``When not calling the mocked function``() = 
    let bert =
        mock<IDealer> "Bert"
          |> setup <@fun x -> x.SellCar @> getCarByPrice

    As bert
      |> Called <@fun x -> x.SellCar @> 19000
      |> Verify

[<Scenario>]
[<FailsWith "Method SellCar was not called with 40000 on Bert.">]
let ``When not calling the second mocked function``() = 
    let bert =
        mock<IDealer> "Bert"
          |> setup <@fun x -> x.SellCar @> getCarByPrice

    As bert
      |> When selling_a_car_for 19000
      |> It should equal LameCar
      |> Whereas bert
      |> Called <@fun x -> x.SellCar @> 19000
      |> Called <@fun x -> x.SellCar @> 40000
      |> Verify
      
[<Scenario>]
let ``When selling the Lamecar for 19000 mocked``() = 
    let bert =
        mock<IDealer> "Bert"
          |> setup <@fun x -> x.SellCar @> getCarByPrice

    As bert
      |> When selling_a_car_for 19000
      |> It should equal LameCar
      |> Verify