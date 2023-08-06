namespace Amazon.Catalog.Adapters.Data.Repositories

module ProductCategoryRepository =
  open Donald
  open System.Data

  open Amazon.Catalog.Adapters.Data
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities

  let insert(prodCat: ProductCategory.T) =
    Database.conn
    |> Db.newCommand "INSERT INTO public.product_category VALUES (@ProductId,@CategoryId)"
    |> Db.setParams [
      "@ProductId", SqlType.Guid prodCat.ProductId
      "@CategoryId", SqlType.Guid prodCat.CategoryId
    ]
    |> Db.exec
    |> function
      | Ok _      -> Ok () 
      | Error err -> err |> Helper.convertDbError