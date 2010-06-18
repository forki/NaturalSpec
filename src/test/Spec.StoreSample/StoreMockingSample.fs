module Spec.StoreSample

open NaturalSpec
open StoreSample.Presenters
open StoreSample.Views
open StoreSample.Services


let creatingPresenterWith view catalog =
    printMethod view
    new ProductsPresenter(catalog, view)
     
[<Scenario>]
let ``ProductsPresenter should set view categories``() =     
    let wasCalled = ref false
    let catalog = 
        mock<ICatalogService> "Catalog"
          |> registerCall <@fun x -> x.GetCategories @>  (fun _ -> [])
    let view = 
        mock<IProductsView> "View"
          |> registerCall <@fun x -> x.SetCategories @>  (fun c -> ())
          |> registerCall <@fun x -> x.CategorySelected.AddHandler @>  (fun c -> ())

    Given catalog
      |> When creatingPresenterWith view
      |> Whereas (!wasCalled)
      |> It should equal true
      // view.Verify(v => v.SetCategories(It.IsAny<IEnumerable<Category>>()));
      |> Verify    