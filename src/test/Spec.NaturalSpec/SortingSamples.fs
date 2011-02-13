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
let ``Sorting []``() =
  quicksortScenario []
    |> Verify
    
[<Scenario>]
let ``Sorting a small list``() =
  quicksortScenario [2;1;8;15;5;22]
    |> Verify      
    
[<Example(100)>]
[<Example(1000)>]
[<Example(2500)>]
let ``Sorting a ordered list`` n =
  quicksortScenario [1..n]
    |> Verify  
    
[<Example(100)>]
[<Example(1000)>]
[<Example(2500)>]
let ``Sorting a random list`` n =
  quicksortScenario (list_of_random_ints n)
    |> Verify
    
                       