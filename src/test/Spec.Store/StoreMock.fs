module Spec.StoreMock

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
    let list = [Category(Id=1);Category(Id=2)] 
    let catalog = 
        mock<ICatalogService> "Catalog"
          |> registerCall <@fun x -> x.GetCategories @> (fun _ -> list)
    let view = 
        mock<IProductsView> "View"
          |> expectCall <@fun x -> x.SetCategories @> list (fun _ -> ())
          |> registerCall <@fun x -> x.CategorySelected.AddHandler @> (fun _ -> ())
    
    Given catalog
      |> When creating_presenter_with view
      |> Verify