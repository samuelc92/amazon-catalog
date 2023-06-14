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

  let create name description price=
    let errors = List.collect (fun (isValid, error) -> if isValid then [] else [error]) [
      (not (String.IsNullOrEmpty name), "Invalid name.")
      (not (String.IsNullOrEmpty description), "Invalid description.")
      (Decimal.IsPositive price, "Invalid price.")]

    match errors with
    | [] -> Ok { Id = Guid.NewGuid(); Name=name; Description=description; Price=price; Active=true }
    | l  -> Error (DomainErrors l)  
