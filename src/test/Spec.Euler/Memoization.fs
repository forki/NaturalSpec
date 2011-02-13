[<AutoOpen>]
module Memoization

open System.Collections.Generic

let memoizeWithDictionary f =
    let cache = Dictionary<_, _>()
    fun x ->
        let (found,result) = cache.TryGetValue(x)
        if found then
            result
        else
            let res = f x
            cache.[x] <- res
            res