module ArticleSearchViewModelSpec

open NaturalSpec

open Articles
open System.Collections.Generic

let starting_search (vm:ArticleSearchViewModel) =
    printMethod ""
    vm.StartSearch()
    vm

let articleCount count (vm:ArticleSearchViewModel) =
    printMethod count
    Seq.length vm.Results = count


[<Scenario>]
let ``Given an existing search term when starting for articles``() =
    let service = 
        mock<IArticleSource> "Service"
            |> setup <@fun x -> x.Search @> (fun x -> if x = "Term" then (new List<Article>()) else failwith "Error")

    let viewModel = new ArticleSearchViewModel(service,SearchTerm = "Term")

    Given viewModel
      |> When starting_search
      |> It should have (articleCount 0)
      |> Verify

            