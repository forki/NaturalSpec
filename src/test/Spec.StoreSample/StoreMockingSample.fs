module Spec.StoreSample

open System
open NaturalSpec
open StoreSample.Models
open StoreSample.Presenter
open StoreSample.Services
open StoreSample.Views

let creating_presenter_with view catalog =
    printMethod view
    new ProductsPresenter(catalog, view)

[<Scenario>]
let ``Creating a ProductsPresenter should set ViewCategories``() =
    let list = [Category(Name="Islay");Category(Name="Highland")] 
    let catalog = 
        mock<ICatalogService> "Catalog"
          |> setup <@fun x -> x.GetCategories @> (fun _ -> list)
    let view = 
        mock<IProductsView> "View"
          |> setup <@fun x -> x.SetCategories @> (fun _ -> ())
          |> setup <@fun x -> x.CategorySelected.AddHandler @> (fun _ -> ())

    Given catalog
      |> When creating_presenter_with view
      |> Whereas view
      |> Called <@fun x -> x.SetCategories @> list
      |> Verify