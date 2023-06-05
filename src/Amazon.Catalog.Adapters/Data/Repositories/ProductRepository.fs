module Amazon.Catalog.Adapters.Data.Repositories

module ProductRepository =
  open Donald
  open Npgsql

  open Amazon.Catalog.Core.Entities

  let conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;")

  let insertAsync(prod: Product.T) =
    conn
    |> Db.newCommand "INSERT INTO public.product VALUES (Id,Name,Description,Price,Active)"
    |> Db.setParams [
      "Id", SqlType.Guid prod.Id
      "Name", SqlType.String prod.Name
      "Description", SqlType.String prod.Description
      "Price", SqlType.Decimal prod.Price
      "Active", SqlType.Boolean prod.Active
    ]
    |> Db.Async.exec
