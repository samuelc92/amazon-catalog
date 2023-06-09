module Amazon.Catalog.WebApi.Program

open Falco
open Falco.Routing
open Falco.HostBuilder

open Amazon.Catalog.Application.Comands
open Amazon.Catalog.Core
open Amazon.Catalog.Adapters.Data.Repositories

let handleError =
  function
  | DbError (message, _) -> Response.withStatusCode 500 >> Response.ofPlainText message
  | NotFoundError _ -> Response.withStatusCode 404 >> Response.ofEmpty  

let handleGenericBadRequest _ =
  Response.withStatusCode 400 >> Response.ofPlainText "Bad request"

let create: HttpHandler =
  let handleCreate req : HttpHandler =
    req 
    |> CreateProductCommand.createProduct
    |> function
      | Ok prod -> prod |> Response.ofJson
      | Error error -> handleError error

  Request.mapJson handleCreate

let getIdFromRoute (routeCollection: RouteCollectionReader) =
  routeCollection.TryGetGuid "id"
  |> function
    | Some id    -> Ok id
    | _                        -> Error "No valid Id provided" 

let getProducts: HttpHandler =
  Request.mapRoute
    (ignore)
    (fun _ ->
      ProductRepository.get
      |> function
        | Ok prods ->
          prods |> Response.ofJson
        | Error error -> handleError error
      )

let getProductsById: HttpHandler =
  fun ctx ->
    let route = Request.getRoute ctx
    route.GetGuid "id"
    |> ProductRepository.getById
    |> function
      | Ok prodOpt ->
        match prodOpt with
          | Some p     -> Response.ofJson p 
          | _         -> handleError NotFoundError
      | Error error -> handleError error

[<EntryPoint>]
let main args =
    webHost args {
        endpoints [
            get "/" (Response.ofPlainText "Hello world Test")

            post "/api/products" create

            get "/api/products" getProducts
        ]
    }
    0