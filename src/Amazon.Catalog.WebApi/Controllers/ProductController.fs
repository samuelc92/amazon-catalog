namespace Amazon.Catalog.WebApi.Controllers

module ProductController =
  open Falco

  open Amazon.Catalog.Adapters.Data.Repositories.ProductRepository
  open Amazon.Catalog.Core
  open Amazon.Catalog.WebApi.Controllers.BaseController
  open Amazon.Catalog.Application.Comands

  let getProducts: HttpHandler = fun ctx ->
    let q = Request.getQuery ctx
    let page = q.GetInt ("page", 0)
    let pageSize = q.GetInt("pageSize", 10)
    let offset = if page = 0 then page else page * pageSize
    handleResponse <| get pageSize offset <| ctx 

  let getProductsById: HttpHandler = fun ctx ->
    let r = Request.getRoute ctx
    let id = r.GetGuid "id" 
    let prod = getById id
    handleResponse prod ctx

  let create: HttpHandler =
    let handleCreate req : HttpHandler =
      req 
      |> CreateProductCommand.handle
      |> handleResponse

    Request.mapJson handleCreate

  let delete: HttpHandler = fun ctx ->
    let r = Request.getRoute ctx
    let id = r.GetGuid "id" 
    let result = DeleteProductCommand.handle id
    handleResponse result ctx

  let update: HttpHandler =
    let handleUpdate (input: UpdateProductCommand.Request): HttpHandler =
      fun ctx -> task {
        let r = Request.getRoute ctx
        let id = r.GetGuid "id"
        if (not (input.Id.Equals id)) then
          handleResponse (Error (DomainError("Invalid id."))) ctx |> ignore
        else
          input
          |>
          UpdateProductCommand.handle
          |> handleResponse <| ctx
          |> ignore
      }

    Request.mapJson handleUpdate
