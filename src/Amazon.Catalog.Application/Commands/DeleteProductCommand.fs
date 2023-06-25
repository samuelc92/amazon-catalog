namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module DeleteProductCommand =
  open Amazon.Catalog.Adapters.Data.Repositories
  let handle id =
    id |> ProductRepository.delete