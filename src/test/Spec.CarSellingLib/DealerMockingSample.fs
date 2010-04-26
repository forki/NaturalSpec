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
let createDealer cars =     
    let dict = cars |> Map.ofSeq
    {new IDealer with 
       member x.SellCar price = Map.find price dict }

// 5. create a method in BDD-style
let selling_a_car_for amount (dealer:IDealer) =
  printMethod amount
  dealer.SellCar amount

// 6. create a scenario      
[<Scenario>]
let When_selling_a_car_for_30000_it_should_equal_the_DreamCar_mocked() =     
  let bert = createDealer [(30000,DreamCar)]
  As bert
    |> When selling_a_car_for 30000
    |> It should equal DreamCar
    |> It shouldn't equal LameCar
    |> Verify
    
     
[<Scenario>]
let When_selling_a_car_for_19000_it_should_equal_the_LameCar_mocked() = 
  let bert = createDealer [(19000,LameCar)]  
  As bert
    |> When selling_a_car_for 19000
    |> It shouldn't equal DreamCar
    |> It should equal LameCar
    |> Verify
    
[<Scenario>]
[<Fails>]
let When_not_calling_the_mocked_function() =   
  let bert = createDealer [(30000,DreamCar);(19000,LameCar)]
  As bert
    |> When selling_a_car_for 19000
    |> It should equal DreamCar
    |> Verify