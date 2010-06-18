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
    let catalog = 
        mock<ICatalogService> "Catalog"
          |> registerCall <@fun x -> x.GetCategories @> (fun _ -> [])
    let view = 
        mock<IProductsView> "View"
          |> registerCall <@fun x -> x.SetCategories @> (fun _ -> ())
          |> registerCall <@fun x -> x.CategorySelected.AddHandler @> (fun _ -> ())
    
    Given catalog
      |> When creating_presenter_with view
      //|>  view.Verify(v => v.SetCategories(It.IsAny<IEnumerable<Category>>()));
      |> Verify