namespace Amazon.Catalog.Core

open System

type Error =
| NotFoundError of string
| DbError of (string * Exception)
| DomainError of string
| DomainErrors of string list

module String =
  let inline empty str = String.IsNullOrWhiteSpace(str)
  let inline noEmpty str = not(empty str) 

module Utils =
  let domainValidate t methods =
    let errors = List.collect (fun (isValid, error) -> if isValid then [] else [error]) methods
    match errors with
      | [] -> Ok t
      | _  -> Error (DomainErrors errors)

  let (>>=) m f = Result.bind f m