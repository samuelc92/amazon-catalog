open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

open Adapters.Repositories
open Entities

Dapper.FSharp.PostgreSQL.OptionTypes.register()

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()

    app.MapGet("/", Func<string>(fun () -> "Hello World!")) |> ignore

    app.MapPost("/", Func<Product.T, unit>(fun (product) -> Product.create product.Name product.Description product.Price |>  ProductRepository.insert)) |> ignore

    app.Run()

    0 // Exit code

