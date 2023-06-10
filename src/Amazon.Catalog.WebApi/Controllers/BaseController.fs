namespace Amazon.Catalog.WebApi.Controllers

module BaseController =
    open Falco
    
    open Amazon.Catalog.Core
 
    let handleError (error: Error) : HttpHandler =
      error
      |> function
        | DbError (message, _) -> Response.withStatusCode 500 >> Response.ofPlainText message
        | NotFoundError _ -> Response.withStatusCode 404 >> Response.ofEmpty  