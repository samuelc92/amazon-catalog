namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module CreateProductCommand =
  open System

  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.Adapters.Data.Repositories

  type Request = { Name: string
                   Description: string
                   Price: decimal }

  let createEntity req : Product.T =
    { Id = Guid.NewGuid()
      Name=req.Name
      Description=req.Description
      Price=req.Price
      Active=true }

  let createProduct (req: Request) =
    createEntity req
    |> Product.validate
    |> Result.bind ProductRepository.insert