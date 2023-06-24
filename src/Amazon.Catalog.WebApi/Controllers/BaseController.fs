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

  let handleError error  : HttpHandler =
    error
    |> function
      | DbError (message, _) -> Response.withStatusCode 500 >> Response.ofPlainText message
      | NotFoundError _ -> Response.withStatusCode 404 >> Response.ofEmpty  
      | DomainError message ->
        Response.withStatusCode 400
        >> Response.ofJsonOptions jsonOption { Title=message; Status=400; Errors=[]}
      | DomainErrors messages ->
        Response.withStatusCode 400
        >> Response.ofJsonOptions jsonOption { Title="One or more validation errors occurred."; Status=400; Errors=messages}

  let handleResponse res =
    match res with
      | Ok body -> body |> Response.ofJsonOptions jsonOption
      | Error error -> error |> handleError