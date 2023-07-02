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
    Utils.domainValidate prod [
      (String.noEmpty prod.Name, "Invalid name.")
      (String.noEmpty prod.Description, "Invalid description.")
      (Decimal.IsPositive prod.Price, "Invalid price.")]