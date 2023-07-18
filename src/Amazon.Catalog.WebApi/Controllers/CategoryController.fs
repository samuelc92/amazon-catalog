namespace Amazon.Catalog.WebApi.Controllers

module CategoryController =
  open Falco

  open Amazon.Catalog.Application.Comands
  open Amazon.Catalog.Adapters.Data.Repositories.CategoryRepository
  open Amazon.Catalog.WebApi.Controllers.BaseController

  let create: HttpHandler =
    let handleCreate req : HttpHandler =
      req 
      |> CreateCategoryCommand.handle
      |> handleResponse

    Request.mapJson handleCreate