module Amazon.Catalog.WebApi.Program

open Falco
open Falco.Routing
open Falco.HostBuilder

open Amazon.Catalog.Adapters.Data.Repositories
open Amazon.Catalog.Application.Comands
open Amazon.Catalog.Core
open Amazon.Catalog.WebApi.Controllers

let handleError (error: Error) : HttpHandler =
  error
  |> function
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
    | Some id   -> Ok id
    | None             -> Error "No valid Id provided" 

let getProductsById: HttpHandler = 
  Request.mapRoute (fun r ->
    r.GetGuid "id")
  |> ProductRepository.getById
  |> function
    | Ok prod ->
      prod |> Response.ofJson
    | Error err -> handleError err
  
[<EntryPoint>]
let main args =
    webHost args {
        endpoints [
            get "/" (Response.ofPlainText "Hello world Test")

            post "/api/products" create

            get "/api/products" ProductController.getProducts
        ]
    }
    0