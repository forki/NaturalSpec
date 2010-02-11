module KnightMovesLib.KnightMoves

open NaturalSpec
open System

type Board = 
  {VisitedFields: (int*int) list;
   Visited: Map<(int*int),int>;
   Length: int;
   Width: int;
   Height: int}

let createBoard n m =
  { VisitedFields = [];
    Length = 0;
    Visited = Map.empty;
    Width = n;
    Height = m}
    
let visited_before field (board:Board) =
  board.Visited |> Map.containsKey field
  
let outside (a,b) board = 
  a < 0 || b < 0 || a >= board.Width || b >= board.Height 
      
let visit field (board:Board) =
  match field with
    | _ when outside field board -> invalidArg "field" "Field coordinates outside the board."
    | _ when visited_before field board -> failwith "Field already visited"
    | a, b -> 
      let newBoard = 
        {board with 
          VisitedFields = field::board.VisitedFields;
          Visited = Map.add field (board.Length+1) board.Visited;
          Length = board.Length+1}
      match board.VisitedFields with
        | [] -> newBoard
        | (x,y)::rest ->
            match abs (x - a), abs (y - b) with
              | 1, 2
              | 2, 1 -> newBoard
              | _    -> failwith <| sprintf "Field %A can't be reached from %A" field (x,y)

let allFields n m =
  [for i in 0..n-1 do
     for j in 0..m-1 do
      yield i,j]              
      
let possible_fields (board:Board) =
  match board.VisitedFields with
    | [] -> allFields board.Width board.Height
    | (x,y)::rest -> 
        let possible f =
          not (outside f board) &&
            not (visited_before f board)
                  
        let fields =
          [for i in [-1;1] do
            for j in [-2;2] do            
              let f1 = x+i,y+j
              let f2 = x+j,y+i
              if possible f1 then yield f1
              if possible f2 then yield f2]
        fields |> List.sort
    
let visit_sequence fields board =
  fields |> Seq.fold (fun b f -> visit f b) board
  
let possible_boards (board:Board)=
    possible_fields board |> List.fold (fun c m -> visit m board :: c) []
    
let targetF b = b.VisitedFields.Length = b.Width * b.Height      
  
let KnightDFS (board:Board) =    
  Optimization.SearchStrategies.DFS targetF possible_boards (visit (0,0) board)
  
let KnightDFS_Stacked (board:Board) =    
  Optimization.SearchStrategies.DFS_Listbased targetF possible_boards (visit (0,0) board)        
  
let KnightBFS (board:Board) =
  Optimization.SearchStrategies.BFS targetF possible_boards (visit (0,0) board)    

let rand = new Random()  
  
let Warnsdorffrule board =
  board
    |> possible_boards
    |> List.map (fun b -> (possible_fields b |> List.length),rand.Next(),b)
    |> List.sort
    |> List.map (fun (m,r,b) -> b)  
    
let WarnsdorffRestarted trials (board:Board) =
  Optimization.SearchStrategies.RestartedSearch targetF Warnsdorffrule trials (visit (0,0) board)
  
let WarnsdorffLDS discrepancy (board:Board) =
  Optimization.SearchStrategies.LDS targetF Warnsdorffrule discrepancy (visit (0,0) board)    