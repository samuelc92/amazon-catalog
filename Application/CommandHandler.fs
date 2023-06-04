module Amazon.Catalog.Application.CommandHandler

open Adapters.Repositories
open Entities

let createProduct (prod: Product.T) =
  ProductRepository.insert prod |> Ok 