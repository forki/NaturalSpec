module Sort.Model

let rec sort list = 
    match list with
    | [] -> list
    | [x] -> list    
    | [x;y] -> if x<y then [x;y] else [y;x]
    | [x;y;z] ->
         sort (List.filter (fun e -> e < x) list) @ 
         [x] @ 
         sort (List.filter (fun e -> e > x) list)    