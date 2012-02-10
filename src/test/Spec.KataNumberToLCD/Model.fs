module KataNumberToLCD.Model

let rec transpose = function
    | (_::_)::_ as M -> List.map List.head M :: transpose (List.map List.tail M)
    | _ -> []

type Segment =
| Blank
| Right
| Left
| Both
| Middle

let segmentToString = function
| Blank    -> "   "
| Right    -> "  |"
| Left     -> "|  "
| Both     -> "| |"
| Middle   -> " - "


let digitsForSegment = function
| 1 -> [Blank; Right; Blank; Right; Blank]
| 2 -> [Middle; Right; Middle; Left; Middle]
| 3 -> [Middle; Right; Middle; Right; Middle]
| 4 -> [Blank; Both; Middle; Right; Blank]
| 5 -> [Middle; Left; Middle; Right; Middle]
| 6 -> [Middle; Left; Middle; Both; Middle]
| 7 -> [Middle; Right; Blank; Right; Blank]
| 8 -> [Middle; Both; Middle; Both; Middle]
| 9 -> [Middle; Both; Middle; Right; Middle]
| _ -> [Middle; Both; Blank; Both; Middle]

let toLCD number =
    let digits = [for c in number.ToString() -> c.ToString()] |> List.map (int)        

    digits 
    |> List.map digitsForSegment 
    |> List.map (List.map segmentToString)
    |> transpose
    |> List.map (String.concat " ")
    |> String.concat "\n"