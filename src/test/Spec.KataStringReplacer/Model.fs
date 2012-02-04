module StringReplacer.Model

let findTemplate (text:string) =
    match text.IndexOf('$') with
    | -1 -> None
    | x  -> 
        match text.IndexOf('$',x+1) with
        | -1 -> None
        | y  -> Some(text.Substring(x,y-x+1))


let replace replacements =
    let replacements = 
        replacements
        |> Seq.map (fun (p,r) -> sprintf "$%s$" p,r)
        |> Map.ofSeq

    let rec replaceAll set text =
        if Set.contains text set then text else
        match findTemplate text with    
        | Some t ->
            let replacement =
                match Map.tryFind t replacements with
                | Some r -> r
                | _ -> ""
            text.Replace(t,replacement)
              |> replaceAll (Set.add text set)
        | None -> text

    replaceAll Set.empty