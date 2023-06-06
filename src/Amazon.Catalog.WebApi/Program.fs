module Amazon.Catalog.WebApi.Program

open Falco
open Falco.Routing
open Falco.HostBuilder

open Amazon.Catalog.Adapters.Data.Repositories
open Amazon.Catalog.Core.Entities

let handleGenericBadRequest _ =
    Response.withStatusCode 400 >> Response.ofPlainText "Bad request"

let handleError =
  function
  | ProductRepository.DbError (message, _) -> Response.withStatusCode 500 >> Response.ofPlainText message

let create : HttpHandler =
  let handleCreate prod : HttpHandler =
    prod
    |> Product.create //TODO: Create a Request type
    |> ProductRepository.insert
    |> function
      | Ok prod -> prod |> Response.ofJson
      | Error error -> handleError error

  Request.mapJson handleCreate

[<EntryPoint>]
let main args =
    webHost args {
        endpoints [
            get "/" (Response.ofPlainText "Hello world Test")

            post "/product" (create)
        ]
    }
    0