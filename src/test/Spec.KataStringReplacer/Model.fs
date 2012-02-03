module StringReplacer.Model

let findTemplate (text:string) =
    match text.IndexOf('$') with
    | -1 -> None
    | x  -> 
        match text.IndexOf('$',x+1) with
        | -1 -> None
        | y  -> Some(text.Substring(x,y-x+1))


let replace replacements (text:string) =
    replacements
      |> Seq.map (fun (p,r) -> sprintf "$%s$" p,r)
      |> Seq.fold (fun (text:string) (p,r) -> text.Replace(p,r)) text
      |> fun s -> s.Replace("$me$","")
      |> fun s -> s.Replace("$really$","")