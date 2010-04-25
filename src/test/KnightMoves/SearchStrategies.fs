module KnightMovesLib.Optimization.SearchStrategies

/// Performs a Depth-First-Search
let DFS targetF childF root =
  let rec dfs node =
    if targetF node then Some(node) else
    childF node
      |> List.tryPick dfs
  dfs root
  
/// Performs a Breadth-First-Search
let BFS targetF childF root =
  let queue = new System.Collections.Generic.Queue<_>()
  queue.Enqueue root
  
  let mutable result = None
  while result = None && queue.Count > 0 do
    let node = queue.Dequeue()
    if targetF node then result <- Some node
    for child in childF node do
      queue.Enqueue child
  result         
  
let DFS_Stacked targetF childF root =
  let stack = new System.Collections.Generic.Stack<_>()
  stack.Push root
  
  let mutable result = None
  while result = None && stack.Count > 0 do
    let node = stack.Pop()
    if targetF node then result <- Some node
    for child in childF node do
      stack.Push child
  result      
  
      
/// Performs a Depth-First-Search
let DFS_Listbased targetF childF root =
  let rec next = function     
    | [] -> None
    | node::rest ->        
        if targetF node then Some node else
        next (childF node @ rest)
  next [root]      
 
  
/// Tries to apply the heuristic function
/// If no target is found a new trial is started  
let RestartedSearch targetF heuristicChildF trials root =
  let rec apply node =      
    match heuristicChildF node with
    | []      -> node
    | x::rest -> apply x
      
  let rec trial trials =
    if trials <= 0 then None else
    let x = apply root
    if targetF x then Some(x) else
    trial (trials-1)
  
  trial trials
     
     
/// Tries to apply the heuristic function
/// "Limited Discrepancy Strategy":
/// If no target is found a 1 discrepany is allowed
/// If no target is found a 2 discrepany is allowed
/// ...
/// If no target is found a maxDiscrepancy discrepany is allowed
let LDS targetF heuristicChildF maxDiscrepancy root =
  let lds_probe maxDiscrepancy =
    let rec getChilds acc childs discrepancy =
      match childs with
      | [] -> acc
      | c::rest ->
          if discrepancy > maxDiscrepancy then acc else
          getChilds ((c,discrepancy)::acc) rest (discrepancy+1)
      
    let rec next stack =      
      match stack with 
      | [] -> None
      | (node,discre)::rest ->        
          if targetF node then Some node else
          let childs = getChilds [] (heuristicChildF node) discre
          next (childs @ rest)
    next [(root,0)]               
  
  let rec lds_trial discrepancy =
    if discrepancy > maxDiscrepancy then None else
    let result = lds_probe 0
    if result <> None then result else
    lds_trial (discrepancy+1)      
  
  lds_trial 0
         