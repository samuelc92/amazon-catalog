namespace Amazon.Catalog.Core.Entities

module Product =
  open System

  open Amazon.Catalog.Core

  type T = { Id: Guid 
             Name: string
             Description: string
             Price: decimal
             //Categories: Category.T list
             //Images: Image.T list
             Active: bool }

  let validate prod =
    let errors = List.collect (fun (isValid, error) -> if isValid then [] else [error]) [
      (not (String.IsNullOrEmpty prod.Name), "Invalid name.")
      (not (String.IsNullOrEmpty prod.Description), "Invalid description.")
      (Decimal.IsPositive prod.Price, "Invalid price.")]

    match errors with
    | [] -> Ok prod
    | l  -> Error (DomainErrors l)  
