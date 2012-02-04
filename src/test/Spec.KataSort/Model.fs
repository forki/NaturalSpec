module Sort.Model

let sort list = 
    match list with
    | [] -> list
    | [x] -> list    
    | x::y::rest -> if x<y then x::y::rest else y::x::rest