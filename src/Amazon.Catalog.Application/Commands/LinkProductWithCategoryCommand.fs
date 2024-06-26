namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module LinkProductWithCategoryCommand =
  open System
   
  open Amazon.Catalog.Adapters.Data.Repositories
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.Core.Utils

  let validateProduct (prodCat: ProductCategory.T) =
    prodCat.ProductId
    |> ProductRepository.getById
    |> function
      | Ok _  -> Ok prodCat 
      | Error err  -> Error err

  let validateCategory(prodCat: ProductCategory.T) =
    prodCat.CategoryId
    |> CategoryRepository.getById
    |> function
      | Ok (Some _)  -> Ok prodCat
      | Ok (None)    -> Error (DomainError "Invalid category type")
      | Error err  -> Error err

  let handle (productId: Guid) (categoryId: Guid) =
    ProductCategory.create productId categoryId
    |> validateProduct
    >>= validateCategory
    >>= ProductCategoryRepository.insert