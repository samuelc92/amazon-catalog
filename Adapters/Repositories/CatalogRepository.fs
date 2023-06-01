namespace Adapters.Repositories

module CatalogRepository =
  open Dapper.FSharp.PostgreSQL
  open Npgsql

  let conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;")  