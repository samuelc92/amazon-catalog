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
  let getProductsById: HttpHandler = fun ctx ->
    let r = Request.getRoute ctx
    let id = r.GetGuid "id" 
    let prod = ProductRepository.getById id
    match prod with
      | Ok prod -> Response.ofJson prod ctx
      | Error err -> handleError err ctx