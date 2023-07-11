namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module UpdateProductCommand =
  open System

  open Amazon.Catalog.Core
  open Amazon.Catalog.Adapters.Data.Repositories
  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.Core.Utils

  type Request = { Id: Guid
                   Name: string
                   Description: string
                   Price: decimal
                   Active: bool }

  let convertToProd req =
    let prodResult = ProductRepository.getById req.Id
    match prodResult with
    | Ok prod ->
      if (not (req.Name.Equals prod.Name)) then
        ProductRepository.getByName req.Name
        |> function
          | Ok result ->
            match result with
            | Some _ -> Error (DomainError ("Product already exists."))
            | None   -> Ok {prod with Name=req.Name; Description=req.Description; Price=req.Price; Active=req.Active} 
          | Error err  -> Error err
      else
        Ok {prod with Name=req.Name; Description=req.Description; Price=req.Price; Active=req.Active} 
    | Error err -> Error err

  let handle (req: Request) =
    req
    |> convertToProd
    >>= Product.validate
    >>= ProductRepository.update