module Amazon.Catalog.WebApi.Program

open Falco
open Falco.Routing
open Falco.HostBuilder
open Microsoft.Extensions.Logging
open Serilog

open Amazon.Catalog.WebApi.Controllers

[<EntryPoint>]
let main args =
  Log.Logger <-
    LoggerConfiguration()
      .MinimumLevel.Debug()
      .Enrich.FromLogContext()
      //.WriteTo.Console(RenderedCompactJsonFormatter())
      .WriteTo.Console()
      .CreateLogger()

  let configureLogging (log: ILoggingBuilder) =
    log.AddSerilog() |> ignore
    log

  webHost args {
    logging configureLogging

    endpoints [
      get "/" (Response.ofPlainText "Ping!")

      post "/api/products"  ProductController.create

      put "/api/products/{id}"  ProductController.update

      delete "/api/products/{id}"  ProductController.delete

      get "/api/products" ProductController.getProducts

      get "/api/products/{id}" ProductController.getProductsById

      post "/api/categories-type"  CategoryTypeController.create

      get "/api/categories-type" CategoryTypeController.getAll

      post "/api/categories" CategoryController.create

      get "/api/categories" CategoryController.getAll

      put "/api/categories/{id}/products/{productId}" CategoryController.linkProduct
    ]
  }
  0