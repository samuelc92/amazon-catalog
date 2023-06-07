namespace Amazon.Catalog.Adapters.Data.Repositories

module ProductRepository =
  open Donald

  open Amazon.Catalog.Adapters.Data
  open Amazon.Catalog.Core.Entities

  let insert(prod: Product.T) =
    Database.conn
    |> Db.newCommand "INSERT INTO public.product VALUES (@Id,@Name,@Description,@Price,@Active)"
    |> Db.setParams [
      "@Id", SqlType.Guid prod.Id
      "@Name", SqlType.String prod.Name
      "@Description", SqlType.String prod.Description
      "@Price", SqlType.Decimal prod.Price
      "@Active", SqlType.Boolean prod.Active
    ]
    |> Db.exec
    |> function
      | Ok _      -> Ok prod
      | Error err -> err |> Helper.convertDbError