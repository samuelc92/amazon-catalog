open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open System
open System.Threading.Tasks

open Amazon.Catalog.Application.CommandHandler
open Amazon.Catalog.Helpers
open Entities

Dapper.FSharp.PostgreSQL.OptionTypes.register()

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()

    app.MapGet("/", Func<string>(fun () -> "Hello World!")) |> ignore

    app.MapPost("/", Func<Product.T, unit>(
      fun (product) ->
        //TODO: Create a request type
        Product.create product.Name product.Description product.Price
        |> createProduct 
        |> Api.ofResult 
        |> Async.AwaitTask )) |> ignore
        
    app.Run()

    0 // Exit code

