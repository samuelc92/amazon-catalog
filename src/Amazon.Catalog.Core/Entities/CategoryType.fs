namespace Amazon.Catalog.Core.Entities

module CategoryType =
  open System
  
  open Amazon.Catalog.Core

  type T = { Id: Guid 
             Name: string
             Description: string
             ShowOnMenu: bool
             ShowOnHome: bool}

  let validate ct=
    let errors = List.collect (fun (isValid, error) -> if isValid then [] else [error]) [
      (String.noEmpty ct.Name, "Invalid name.")
      (String.noEmpty ct.Description, "Invalid description.")]

    match errors with
    | [] -> Ok ct
    | l  -> Error (DomainErrors l)  