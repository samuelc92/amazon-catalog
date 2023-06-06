namespace Amazon.Catalog.Adapters.Data

module Helper =

  open Donald
  open System

  open Amazon.Catalog.Core

  let convertDbError err =
    match err with
    | DbExecutionError err -> Error (DbError (err.Statement, err.Error :> Exception))
    | DbConnectionError err -> Error (DbError ("Invalid connection string", err.Error))
    | DbTransactionError err -> Error (DbError ("Transaction error", err.Error))
    | DataReaderCastError err -> Error (DbError (err.FieldName, err.Error :> Exception))
    | DataReaderOutOfRangeError err -> Error (DbError (err.FieldName, err.Error :> Exception))