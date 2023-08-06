namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module LinkProductWithCategoryCommand =
  open System
   
  open Amazon.Catalog.Adapters.Data.Repositories
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.Core.Utils

  let handle (productsId: Guid list) (categoryId: Guid) =
    productsId
    |> List.map (fun productId -> { ProductId = productId; CategoryId = categoryId })
    |> ProductCategoryRepository.insert