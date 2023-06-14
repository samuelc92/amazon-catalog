namespace Amazon.Catalog.WebApi.Controllers

type ProblemDetails = { Title: string
                        Status: int 
                        Errors: string list }
module BaseController =
  open Falco
    
  open Amazon.Catalog.Core
  open System.Text.Json
 
  let jsonOption =
    let option= JsonSerializerOptions()
    option.PropertyNamingPolicy <- JsonNamingPolicy.CamelCase
    option
    
  let handleError (error: Error) : HttpHandler =
    error
    |> function
      | DbError (message, _) -> Response.withStatusCode 500 >> Response.ofPlainText message
      | NotFoundError _ -> Response.withStatusCode 404 >> Response.ofEmpty  
      | DomainError message -> Response.withStatusCode 400 >> Response.ofPlainText message

  let handleErrors (errors: Error list) : HttpHandler =
    let problemDetails = {
      Title = "One or more validation errors occurred."
      Status = 400
      Errors = List.map (fun (err: Error) -> match err with | DomainError message -> message | _ -> "Internal server error.") errors 
    }
    Response.withStatusCode 400 >> Response.ofJsonOptions jsonOption problemDetails