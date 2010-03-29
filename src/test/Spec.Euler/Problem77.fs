module Problem77
  
open NaturalSpec

/// Problem 77
/// It is possible to write ten as the sum of primes in exactly five different ways:
/// 
/// 7 + 3
/// 5 + 5
/// 5 + 3 + 2
/// 3 + 3 + 2 + 2
/// 2 + 2 + 2 + 2 + 2
/// 
/// What is the first value which can be written as the sum of primes in over five thousand different ways?
let FindSmallestValue count =
    printMethod ""
    let p = primes 5000I
    let cache = new System.Collections.Generic.Dictionary<_,_>()    
    let p' = new System.Collections.Generic.HashSet<_>()
    p |> Seq.iter (p'.Add >> ignore)
    let isPrime = p'.Contains

    let rec possibilities n =
      if n <= 1I then Set.empty else 
      match cache.TryGetValue n with
      | true, m -> m
      | _ ->
          let m =
              p 
                |> Seq.takeWhile (fun x -> x <= n)
                |> Seq.fold (fun acc x ->                       
                      possibilities (n-x)
                        |> Set.map (fun l -> x::l |> List.sort)
                        |> Set.union acc)
                      Set.empty
          let m' = if isPrime n then Set.add [n] m else m
          cache.Add(n,m')
          m'

    allNumbersGreaterThan 4
      |> Seq.find (fun x -> 
           let m = x |> bigint |> possibilities
           Set.count m > count)

[<Scenario>]      
let ``What is the first value which can be written as the sum of primes in over 4 different ways?``() =
    Given 4
      |> When solving FindSmallestValue
      |> It should equal 10
      |> Verify

[<Scenario>]      
let ``What is the first value which can be written as the sum of primes in over five thousand different ways?``() =
    Given 5000
      |> When solving FindSmallestValue
      |> It should equal 71
      |> Verify