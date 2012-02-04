module Sort.Model

let sort list = 
    match list with
    | [] -> list
    | [x] -> list
    | [x;y] -> if x<y then [x;y] else [y;x]