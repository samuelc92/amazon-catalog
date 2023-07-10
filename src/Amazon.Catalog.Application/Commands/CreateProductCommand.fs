namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module CreateProductCommand =
  open System
   
  open Amazon.Catalog.Adapters.Data.Repositories
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.Core.Utils

  type Request = { Name: string
                   Description: string
                   Price: decimal }

  let createEntity req : Product.T =
    { Id          = Guid.NewGuid()
      Name        = req.Name
      Description = req.Description
      Price       = req.Price
      Active      = true }

  let checkIfProductExist (prod: Product.T) =
    ProductRepository.getByName prod.Name
    |> function
      | Ok result ->
        match result with
        | Some _ -> Error (DomainError ("Product already exists."))
        | None   -> Ok prod
      | Error err  -> Error err

  let handle (req: Request) =
    createEntity req
    |> Product.validate
    >>= checkIfProductExist
    >>= ProductRepository.insert