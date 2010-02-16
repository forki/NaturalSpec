module NaturalSpec.ListSamples

[<Scenario>]
let When_creating_a_empty_list_it_should_be_empty() =
  Given []
    |> It should be empty
    |> It should have (length 0)
    |> Verify
    
[<Scenario>]
let When_creating_a_nonempty_array_it_should_not_be_empty() =
  Given [|1;2|]
    |> It shouldn't be empty
    |> Verify
    
[<Scenario>]
let When_creating_a_list_with_1_and_2_it_should_contain_1() =
  Given [1;2]
    |> It should contain 1
    |> Verify
    
[<Scenario>]
let When_creating_a_list_with_1_and_2_it_should_not_contain_3() =
  Given [1;2]
    |> It shouldn't contain 3
    |> It should have (length 2)
    |> It shouldn't have duplicates
    |> Verify
    
[<Scenario>]
let When_creating_a_list_with_2_and_2_it_should_contain_duplicates() =
  Given [-1;0;2;3;2;1;4;5]
    |> It should have (length 8)
    |> It should have duplicates
    |> Verify      

[<Scenario>]
let When_removing_an_3_from_a_small_list_it_should_not_contain_3() =
  Given [1;2;3;4;5]              // Arrange test context
    |> When Removing 3           // Act
    |> It shouldn't contain 3    // Assert
    |> It should have (length 4) // Assert      
    |> It should contain 4       // Assert
    |> Verify                    // Verify scenario