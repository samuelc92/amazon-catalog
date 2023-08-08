namespace Amazon.Catalog.WebApi.Controllers

module CategoryController =
  open Falco

  open Amazon.Catalog.Application.Comands
  open Amazon.Catalog.Adapters.Data.Repositories.CategoryRepository
  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.WebApi.Controllers.BaseController

  let create: HttpHandler =
    let handleCreate req : HttpHandler =
      req 
      |> CreateCategoryCommand.handle
      |> handleResponse

    Request.mapJson handleCreate

  let getAll: HttpHandler = fun ctx ->
    let q = Request.getQuery ctx
    let page = q.GetInt ("page", 0)
    let pageSize = q.GetInt("pageSize", 10)
    let offset = if page = 0 then page else page * pageSize
    handleResponse <| get pageSize offset <| ctx

  let linkProduct: HttpHandler = fun ctx ->
    let r = Request.getRoute ctx
    let id = r.GetGuid "id" 
    let prodId = r.GetGuid "productId"
    LinkProductWithCategoryCommand.handle prodId id
    |> handleResponse <| ctx