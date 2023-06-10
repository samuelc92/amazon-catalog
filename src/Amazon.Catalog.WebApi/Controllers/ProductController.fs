namespace Amazon.Catalog.WebApi.Controllers

module ProductController =
  open Falco

  open Amazon.Catalog.Adapters.Data.Repositories
  open Amazon.Catalog.WebApi.Controllers.BaseController

  let getProducts: HttpHandler =
    Request.mapRoute
      (ignore)
      (fun _ ->
        ProductRepository.get
        |> function
          | Ok prods ->
            prods |> Response.ofJson
          | Error error -> handleError error
        )