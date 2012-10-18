module StringReplacer.Model

let findFirstPattern (text:string) =
    match text.IndexOf('$') with
    | -1 -> None
    | x  -> 
        match text.IndexOf('$',x+1) with
        | -1 -> None
        | y  -> Some(text.Substring(x,y-x+1))

let replace dict (text:string) =
    let rec replace texts (text:string) =
        if Set.contains text texts then text else
        match findFirstPattern text with
        | None -> text
        | Some pattern ->
            match dict |> Map.tryFind (pattern.Replace("$","")) with
            | Some(replacement) -> text.Replace(pattern,replacement)
            | None ->              text.Replace(pattern,"")
            |> replace (Set.add text texts) 

    replace Set.empty text