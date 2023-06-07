namespace Amazon.Catalog.Adapters.Data

module Database =
  open Npgsql

  let conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;")