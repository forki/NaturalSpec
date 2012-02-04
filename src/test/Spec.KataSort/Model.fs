module Sort.Model

let rec sort list = 
    match list with
    | [] -> list 
    | x::rest ->
         let smaller,greater = List.partition (fun e -> e <= x) rest
         sort smaller @ [x] @ sort greater