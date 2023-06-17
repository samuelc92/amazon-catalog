module Amazon.Catalog.WebApi.Program

open Falco
open Falco.Routing
open Falco.HostBuilder

open Amazon.Catalog.WebApi.Controllers

[<EntryPoint>]
let main args =
    webHost args {
        endpoints [
            get "/" (Response.ofPlainText "Ping!")

            post "/api/products"  ProductController.create

            delete "/api/products/{id}"  ProductController.delete

            get "/api/products" ProductController.getProducts

            get "/api/products/{id}" ProductController.getProductsById
        ]
    }
    0