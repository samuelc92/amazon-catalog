namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module UpdateProductCommand =
  open System

  open Amazon.Catalog.Adapters.Data.Repositories

  type Request = { Id: Guid
                   Name: string
                   Description: string
                   Price: decimal
                   Active: bool }

  let handle (req: Request) =
    req