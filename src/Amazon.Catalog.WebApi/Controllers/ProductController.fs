namespace Amazon.Catalog.WebApi.Controllers

module ProductController =
  open Falco

  open Amazon.Catalog.Adapters.Data.Repositories.ProductRepository
  open Amazon.Catalog.WebApi.Controllers.BaseController
  open Amazon.Catalog.Application.Comands

  let getProducts: HttpHandler =
    Request.mapRoute
      (ignore)
      (fun _ ->
        get
        |> function
          | Ok prods ->
            prods |> Response.ofJsonOptions jsonOption
          | Error error -> handleError error
        )
  let getProductsById: HttpHandler = fun ctx ->
    let r = Request.getRoute ctx
    let id = r.GetGuid "id" 
    let prod = getById id
    match prod with
      | Ok prod -> Response.ofJsonOptions jsonOption prod ctx
      | Error err -> handleError err ctx

  let create: HttpHandler =
    let handleCreate req : HttpHandler =
      req 
      |> CreateProductCommand.createProduct
      |> function
        | Ok prod -> prod |> Response.ofJsonOptions jsonOption
        | Error error -> handleError error

    Request.mapJson handleCreate