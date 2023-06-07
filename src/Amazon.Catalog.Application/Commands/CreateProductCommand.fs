namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module CreateProductCommand =
  open Amazon.Catalog.Core.Entities

  type Request = { Name: string
                   Description: string
                   Price: decimal }

  let createProduct (req: Request) =
    Product.create req.Name req.Description req.Price