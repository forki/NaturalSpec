// 1. define module
module Spec.CarSellingLib.DealerMockingSample

// 2. open NaturalSpec-Namespace
open NaturalSpec
        
// 3. open project namespace
open CarSellingLib

// define reusable values
let DreamCar = new Car(CarType.BMW, 200)
let LameCar = new Car(CarType.Fiat, 45)

// 4. define a mock object and give it a name
let createDealer carPrices =     
    let dict = Map.ofSeq carPrices
    mock<IDealer> "Bert"
       |> register <@fun x -> x.SellCar @> (fun price -> Map.find price dict)    

// 5. create a method in BDD-style
let selling_a_car_for amount (dealer:IDealer) =
    printMethod amount
    dealer.SellCar amount

// 6. create a scenario      
[<Scenario>]
let ``When selling the DreamCar for 40000``() =     
    let bert = createDealer [(40000,DreamCar)]

    As bert
      |> When selling_a_car_for 40000
      |> It should equal DreamCar
      |> It shouldn't equal LameCar
      |> Verify
    
     
[<Scenario>]
let ``When selling the Lamecar for 19000``() = 
    let bert = createDealer [(19000,LameCar)]  

    As bert
      |> When selling_a_car_for 19000
      |> It shouldn't equal DreamCar
      |> It should equal LameCar
      |> Verify