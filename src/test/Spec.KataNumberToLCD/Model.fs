module KataNumberToLCD.Model

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


let toLCD number =
    let segments =
        match number with
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
    segments 
    |> Seq.map segmentToString
    |> Seq.fold (fun s x -> s + x + "\n") ""