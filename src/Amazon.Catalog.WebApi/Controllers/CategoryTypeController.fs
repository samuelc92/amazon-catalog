namespace Amazon.Catalog.WebApi.Controllers

module CategoryTypeController =
  open Falco

  open Amazon.Catalog.WebApi.Controllers.BaseController
  open Amazon.Catalog.Application.Comands


  let create: HttpHandler =
    let handleCreate req : HttpHandler =
      req 
      |> CreateCategoryTypeCommand.handle
      |> handleResponse

    Request.mapJson handleCreate