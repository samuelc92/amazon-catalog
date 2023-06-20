namespace Amazon.Catalog.WebApi.Controllers

module ProductController =
  open Falco

  open Amazon.Catalog.Adapters.Data.Repositories.ProductRepository
  open Amazon.Catalog.WebApi.Controllers.BaseController
  open Amazon.Catalog.Application.Comands

  let getProducts: HttpHandler = fun ctx ->
    let q = Request.getQuery ctx
    let page = q.GetInt ("page", 0)
    let pageSize = q.GetInt("pageSize", 10)
    let offset = if page = 0 then page else page * pageSize
    get pageSize offset 
    |> function
      | Ok prods -> Response.ofJsonOptions jsonOption prods ctx 
      | Error error -> handleError error ctx

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
      |> CreateProductCommand.handle
      |> function
        | Ok prod -> prod |> Response.ofJsonOptions jsonOption
        | Error error -> handleError error

    Request.mapJson handleCreate

  let delete: HttpHandler = fun ctx ->
    let r = Request.getRoute ctx
    let id = r.GetGuid "id" 
    let result = DeleteProductCommand.handle id
    match result with
      | Ok _ -> Response.ofEmpty ctx
      | Error err -> handleError err ctx