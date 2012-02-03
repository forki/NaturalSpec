module StringReplacer.Model


let replace replacements (text:string) =
    replacements
      |> Seq.map (fun (p,r) -> sprintf "$%s$" p,r)
      |> Seq.fold (fun (text:string) (p,r) -> text.Replace(p,r)) text
      |> fun s -> s.Replace("$me$","")