namespace Adapters.Repositories

module ProductRepository =
  open Dapper.FSharp.PostgreSQL
  open Npgsql

  open Amazon.Catalog.Helpers
  open Entities

  let conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;")

  let productTable = table'<Product.T> "product"

  let insert product =
    insert {
      into productTable
      value product
    }
    |> conn.InsertAsync
    |> Task.ignore