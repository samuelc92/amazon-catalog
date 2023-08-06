namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module LinkProductWithCategoryCommand =
  open System
   
  open Amazon.Catalog.Adapters.Data.Repositories
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities.ProductCategory
  open Amazon.Catalog.Core.Utils

  let validateProduct (prodCat: T) =
    prodCat.ProductId
    |> ProductRepository.getById
    |> function
      | Ok _  -> Ok prodCat 
      | Error err  -> Error err

  let handle (productId: Guid) (categoryId: Guid) =
    { ProductId = productId; CategoryId = categoryId }
    |> validateProduct
    >>= ProductCategoryRepository.insert