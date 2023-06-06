module Amazon.Catalog.Adapters.Data.Repositories

module ProductRepository =
  open Donald
  open Npgsql
  open System

  open Amazon.Catalog.Core.Entities

  let conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;")

  type Error =
    | NotFoundError of string
    | DbError of (string * Exception)

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
      | Error error ->
        match error with
          | DbExecutionError err -> Error (DbError (err.Statement, err.Error :> Exception))
          | DbConnectionError err -> Error (DbError (err.ConnectionString, err.Error))
          | DbTransactionError err -> Error (DbError ("Transaction error", err.Error))
          | DataReaderCastError err -> Error (DbError (err.FieldName, err.Error :> Exception))
          | DataReaderOutOfRangeError err -> Error (DbError (err.FieldName, err.Error :> Exception))
