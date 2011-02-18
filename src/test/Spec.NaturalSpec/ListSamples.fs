module NaturalSpec.ListSamples

[<Scenario>]
let ``A empty list should be empty``() =
  Given []
    |> It should be empty
    |> It should have (length 0)
    |> Verify
    
[<Scenario>]
let ``A nonempty array should not be empty``() =
  Given [|1;2|]
    |> It shouldn't be empty
    |> Verify
    
[<Scenario>]
let ``A list with 1 and 2 should contain 1``() =
  Given [1;2]
    |> It should contain 1
    |> Verify
    
[<Scenario>]
let ``A list with 1 and 2 it should not contain 3``() =
  Given [1;2]
    |> It shouldn't contain 3
    |> It should have (length 2)
    |> It shouldn't have duplicates
    |> Verify
    
[<Scenario>]
let ``A list with two 2's should contain duplicates``() =
  Given [-1;0;2;3;2;1;4;5]
    |> It should have (length 8)
    |> It should have duplicates
    |> Verify      

[<Scenario>]
let ``After removing 3 from a list it should not contain 3``() =
  Given [1;2;3;4;5]              // Arrange test context
    |> When removing 3           // Act
    |> It shouldn't contain 3    // Assert
    |> It should have (length 4) // Assert      
    |> It should contain 4       // Assert
    |> Verify                    // Verify scenario