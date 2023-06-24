module Amazon.Catalog.WebApi.Program

open Falco
open Falco.Routing
open Falco.HttpHandler
open Falco.HostBuilder
open Microsoft.Extensions.Logging
open Serilog

open Amazon.Catalog.WebApi.Controllers
open Amazon.Catalog.Core.Entities

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
    ]
  }
  0