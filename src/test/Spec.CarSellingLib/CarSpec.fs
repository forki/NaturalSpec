// 1. define module
module Spec.CarSellingLib.CarSpec

// 2. open NaturalSpec-Namespace
open NaturalSpec
        
// 3. open project namespace
open CarSellingLib

// 4. define a test context
let Bert = new Dealer("Bert")

// define reusable values
let DreamCar = new Car(CarType.BMW, 200)
let LameCar = new Car(CarType.Fiat, 45)

// 5. create a method in BDD-style
let selling_a_car_for amount (dealer:Dealer) =
  printMethod amount
  dealer.SellCar amount

// 6. create a scenario      
[<Scenario>]
let When_selling_a_car_for_30000_it_should_equal_the_DreamCar() =
  As Bert
    |> When selling_a_car_for 30000
    |> It should equal DreamCar
    |> It shouldn't equal LameCar
    |> Verify      
    
[<Scenario>]
let When_selling_a_car_for_19000_it_should_equal_the_LameCar() =
  As Bert
    |> When selling_a_car_for 19000
    |> It should equal LameCar
    |> It shouldn't equal DreamCar
    |> Verify      
    
[<Scenario>]
[<FailsWith "Need more money">]
let When_selling_a_car_for_1000_it_should_fail_with_Need_More_Money() =
  As Bert
    |> When selling_a_car_for 1000
    |> Verify     
  
  
let sellingScenario dealer amount car =
  As dealer
    |> When selling_a_car_for amount
    |> It should equal car 
    
[<Scenario>]
let When_using_predefined_car_selling_scenario() =
  sellingScenario Bert 19000 LameCar |> Verify               