module NaturalSpec.SortingSamples

/// naive implementation of QuickSort - don't use it
let rec quicksort = function
  | [] -> []
  | pivot :: rest ->
     let small,big = List.partition ((>) pivot) rest
     quicksort small @ [pivot] @ quicksort big       
     
let QuickSort x =
  printMethod ""
  quicksort x   
   
/// predefined sorting scenario
let sortingScenario f list =
  Given list
    |> When sorting_with f
    |> It should be sorted
    |> It should contain_all_elements_from list
    |> It should contain_no_other_elements_than list
    
/// predefined Quicksort scenario
let quicksortScenario list = sortingScenario QuickSort list

[<Scenario>]
let When_sorting_empty_list() =
  quicksortScenario []
    |> Verify
    
[<Scenario>]
let When_sorting_small_list() =
  quicksortScenario [2;1;8;15;5;22]
    |> Verify      
    
[<ScenarioTemplate(100)>]
[<ScenarioTemplate(1000)>]
[<ScenarioTemplate(2500)>]
let When_sorting_ordered_list n =
  let list = List.init n (fun i -> i) 
  quicksortScenario list
    |> Verify  
    
[<ScenarioTemplate(100)>]
[<ScenarioTemplate(1000)>]
[<ScenarioTemplate(2500)>]
let When_sorting_random_list n =
  quicksortScenario (list_of_random_ints n)
    |> Verify
    
                       