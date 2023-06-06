namespace Amazon.Catalog.Adapters.Data.Repositories

module ProductRepository =
  open Donald
  open Npgsql
  open System

  open Amazon.Catalog.Adapters.Data
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities

  let conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;")


  let insert(prod: Product.T) =
    conn
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