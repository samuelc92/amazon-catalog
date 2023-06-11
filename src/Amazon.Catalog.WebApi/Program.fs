module Amazon.Catalog.WebApi.Program

open Falco
open Falco.Routing
open Falco.HostBuilder

open Amazon.Catalog.Core
open Amazon.Catalog.WebApi.Controllers

let handleError (error: Error) : HttpHandler =
  error
  |> function
    | DbError (message, _) -> Response.withStatusCode 500 >> Response.ofPlainText message
    | NotFoundError _ -> Response.withStatusCode 404 >> Response.ofEmpty  

[<EntryPoint>]
let main args =
    webHost args {
        endpoints [
            get "/" (Response.ofPlainText "Ping!")

            post "/api/products"  ProductController.create

            get "/api/products" ProductController.getProducts

            get "/api/products/{id}" ProductController.getProductsById
        ]
    }
    0