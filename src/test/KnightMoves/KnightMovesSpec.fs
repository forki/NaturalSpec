module KnightMovesLib.KnightMovesSpec

open NaturalSpec
open System

open KnightMoves
  
let have_width n (x:Board) =
  printMethod n
  Equality,n,x.Width,x
  
let have_height n (x:Board) =
  printMethod n
  Equality,n,x.Height,x    
  
let visiting field board =
  printMethod field
  visit field board
  
let visiting_sequence fields board =
  printMethod fields
  visit_sequence fields board
  
let getting_possible_fields board =
  printMethod ""
  possible_fields board
          
let have_visited field board =
  printMethod field
  IsTrue,true,(visited_before field board),board
  
let have_visited_all_from seq board =
  contain_all_elements_from seq board.VisitedFields
  
let sizes = TestWithList [0;1;2;4;8;16]
  
[<ScenarioSource "sizes">]
let When_init_board size =
  Given (createBoard size size)
    |> It should have_width size
    |> It should have_height size
    |> Verify
    
let visits =
  let fail x = x |> ShouldFailWith (typeof<ArgumentException>)
  TestWith (tripleParam 8 0 0)
    |> And (tripleParam 8 1 1)
    |> And (tripleParam 8 -1 0) |> fail
    |> And (tripleParam 8 0 -1) |> fail
    |> And (tripleParam 8 8 0)  |> fail
    |> And (tripleParam 8 0 8)  |> fail
    |> And (tripleParam 8 8 8)  |> fail
    
[<ScenarioSource "visits">]
let When_visiting_field size x y =
  Given (createBoard size size)
    |> When visiting (x,y)
    |> It should have_width size
    |> It should have_visited (x,y)
    |> Verify
    
    
let visitSequences =
  let fail x = x |> ShouldFailWith (typeof<ArgumentException>)
  let failure x = x |> ShouldFailWith (typeof<Exception>)
  
  TestWith (doubleParam 8 [(0,0);(1,2);(2,0);(3,2);(5,3)])
    |> And (doubleParam 8 [(0,0);(2,0);(3,4)])        |> failure
    |> And (doubleParam 8 [(0,0);(1,2);(2,0);(4,-1)]) |> fail
    |> And (doubleParam 8 [(0,0);(-1,2)])             |> fail
    |> And (doubleParam 8 [(7,5);(5,4);(4,6);(2,7)])
    |> And (doubleParam 8 [(7,5);(5,4);(4,6);(5,8)])  |> fail      
    |> And (doubleParam 8 [(7,5);(5,4);(4,6);(3,8)])  |> fail      
    |> And (doubleParam 8 [(7,5);(8,3)])              |> fail      
    |> And (doubleParam 8 [(7,5);(6,3)])
    |> And (doubleParam 8 [(7,5);(6,3);(5,2)])        |> failure
    |> And (doubleParam 8 [(7,5);(6,3);(4,2)])
    |> And (doubleParam 8 [(7,5);(6,3);(4,2);(1,1)])  |> failure
    |> And (doubleParam 8 [(7,5);(6,3);(4,2);(6,3)])  |> failure
          
[<ScenarioSource "visitSequences">]
let When_visiting_fields size seq =
  Given (createBoard size size)
    |> When visiting_sequence seq
    |> It should have_visited_all_from seq
    |> Verify            
    
    
let possibleFieldsTestData =
  let fail x = x |> ShouldFailWith (typeof<ArgumentException>)
  let empty:(int*int) list = []
  
  TestWith (doubleParam empty (allFields 8 8)) |> Named "empty"
    |> And (doubleParam [(0,0)] [(1,2);(2,1)]) |> Named "(0,0)"
    |> And (doubleParam [(8,8)] empty) |> fail |> Named "(8,8)"
    |> And (doubleParam [(7,7)] [(5,6);(6,5)]) |> Named "(7,7)"
    |> And (doubleParam [(7,3)] [(5,2);(5,4);(6,1);(6,5)]) |> Named "(7,3)"
          
[<ScenarioSource "possibleFieldsTestData">]
let When_getting_possible_fields seq possibleFields =    
  Given (createBoard 8 8 |> visit_sequence seq)
    |> When getting_possible_fields
    |> It should equal (List.sort possibleFields)
    |> Verify               
  
let real_KnightMove board =
  printMethod ""
  match board with
    | None          -> false
    | Some(b:Board) -> b.Width*b.Height = b.VisitedFields.Length
        
let dfsSizes = TestWithList [5;6]  
  
let getting_KnightMoves_with_DFS (board:Board) =
  printMethod ""
  KnightDFS board   
      
[<ScenarioSource "dfsSizes">]
let When_getting_KnightMoves_with_DFS n =    
  Given (createBoard n n)
    |> When getting_KnightMoves_with_DFS
    |> It shouldn't equal None
    |> It should be real_KnightMove
    |> Verify  
    
let getting_KnightMoves_with_DFS_Stacked (board:Board) =
  printMethod ""
  KnightDFS_Stacked board   
      
[<ScenarioSource "dfsSizes">]
let When_getting_KnightMoves_with_DFS_Stacked n =    
  Given (createBoard n n)
    |> When getting_KnightMoves_with_DFS_Stacked
    |> It shouldn't equal None
    |> It should be real_KnightMove
    |> Verify                
    
let getting_KnightMoves_with_BFS (board:Board) =
  printMethod ""
  KnightBFS board   
        
[<Scenario>]
let When_getting_KnightMoves_with_BFS()=    
  Given (createBoard 4 4)
    |> When getting_KnightMoves_with_BFS
    |> It should equal None
    |> Verify            
    
let getting_KnightMoves_with_Warnsdorff_restarted trials (board:Board) =
  printMethod (sprintf "(%d trials)" trials)
  WarnsdorffRestarted trials board    

let warnsdorffSizes = TestWithList [6;8;10;12;16;20;32;50]  
  
[<ScenarioSource "warnsdorffSizes">]
let When_getting_KnightMoves_with_Warnsdorff_restarted n =    
  Given (createBoard n n)
    |> When (getting_KnightMoves_with_Warnsdorff_restarted 10)
    |> It shouldn't equal None
    |> It should be real_KnightMove      
    |> Verify           
    
let warnsdorffSmallSizes = TestWithList [8;10]  
    
[<ScenarioSource "warnsdorffSmallSizes">]
[<Fails>]
let When_getting_KnightMoves_with_Warnsdorff_restarted_can_fail n =    
  for i in 1..1000 do
    Given (createBoard n n)
      |> When (getting_KnightMoves_with_Warnsdorff_restarted 1)
      |> It shouldn't equal None
      |> It should be real_KnightMove
      |> Verify        
      
let getting_KnightMoves_with_Warnsdorff_LDS discrepancy (board:Board) =
  printMethod (sprintf "(%d discrepancy)" discrepancy)
  WarnsdorffLDS discrepancy board    

  
[<ScenarioSource "warnsdorffSizes">]
let When_getting_KnightMoves_with_Warnsdorff_LDS n =    
  Given (createBoard n n)
    |> When (getting_KnightMoves_with_Warnsdorff_LDS 3)
    |> It shouldn't equal None
    |> It should be real_KnightMove      
    |> Verify           