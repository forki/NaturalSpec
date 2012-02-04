module Sort.Model

let sort list = 
    match list with
    | [] -> list
    | [x] -> list    
    | [x;y] -> if x<y then [x;y] else [y;x]
    | [x;y;z] when x<=y && y <=z -> [x;y;z]
    | [x;y;z] when x<=z && z <=y -> [x;z;y]
    | [x;y;z] when y<=x && x <=z -> [y;x;z]
    | [x;y;z] when y<=z && z <=x -> [y;z;x]
    | [x;y;z] when z<=x && x <=y -> [z;x;y]
    | [x;y;z] when z<=y && y <=x -> [z;y;x]