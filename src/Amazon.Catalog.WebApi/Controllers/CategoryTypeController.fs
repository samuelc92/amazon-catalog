namespace Amazon.Catalog.WebApi.Controllers

module CategoryTypeController =
  open Falco

  open Amazon.Catalog.Application.Comands
  open Amazon.Catalog.Adapters.Data.Repositories.CategoryTypeRepository
  open Amazon.Catalog.WebApi.Controllers.BaseController

  let create: HttpHandler =
    let handleCreate req : HttpHandler =
      req 
      |> CreateCategoryTypeCommand.handle
      |> handleResponse

    Request.mapJson handleCreate

  let getAll: HttpHandler = fun ctx ->
    let q = Request.getQuery ctx
    let page = q.GetInt ("page", 0)
    let pageSize = q.GetInt("pageSize", 10)
    let offset = if page = 0 then page else page * pageSize
    handleResponse <| get pageSize offset <| ctx 