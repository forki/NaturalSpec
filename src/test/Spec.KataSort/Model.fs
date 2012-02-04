module Sort.Model

let rec sort list = 
    match list with
    | [] -> list 
    | x::rest ->
         sort (List.filter (fun e -> e <= x) rest) @
         [x] @ 
         sort (List.filter (fun e -> e > x) rest)