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
let Bert = mock<IDealer> "Bert"

// 5. create a method in BDD-style
let selling_a_car_for amount (dealer:IDealer) =
  printMethod amount
  dealer.SellCar amount

// 6. create a scenario      
[<Scenario>]
let When_selling_a_car_for_30000_it_should_equal_the_DreamCar_mocked() =     
  As Bert
    |> Mock Bert.SellCar 30000 DreamCar  // register mocked call
    |> When selling_a_car_for 30000
    |> It should equal DreamCar
    |> It shouldn't equal LameCar
    |> Verify
    
     
[<Scenario>]
let When_selling_a_car_for_19000_it_should_equal_the_LameCar_mocked() =   
  As Bert
    |> Mock Bert.SellCar 19000 LameCar
    |> When selling_a_car_for 19000
    |> It shouldn't equal DreamCar
    |> It should equal LameCar
    |> Verify
    
[<Scenario>]
[<Fails>]
let When_not_calling_the_mocked_function() =   
  As Bert
    |> Mock Bert.SellCar 30000 DreamCar
    |> Mock Bert.SellCar 19000 DreamCar
    |> When selling_a_car_for 19000
    |> It should equal DreamCar
    |> Verify