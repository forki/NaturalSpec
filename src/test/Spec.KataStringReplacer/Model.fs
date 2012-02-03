module StringReplacer.Model

let findTemplate (text:string) =
    match text.IndexOf('$') with
    | -1 -> None
    | x  -> 
        match text.IndexOf('$',x+1) with
        | -1 -> None
        | y  -> Some(text.Substring(x,y-x+1))


let replace replacements (text:string) =
    let replacements = 
        replacements
        |> Seq.map (fun (p,r) -> sprintf "$%s$" p,r)
        |> Map.ofSeq

    let rec replaceAll text =
        match findTemplate text with    
        | Some t ->
            match Map.tryFind t replacements with
            | Some r -> replaceAll (text.Replace(t,r))
            | _ -> replaceAll (text.Replace(t,""))
        | None -> text

    replaceAll text