namespace Amazon.Catalog.Core.Entities

module CategoryType =
  open System
  
  open Amazon.Catalog.Core

  type T = { Id: Guid 
             Name: string
             Description: string
             ShowOnMenu: bool
             ShowOnHome: bool }

  let validate ct=
    Utils.domainValidate ct [
      (String.noEmpty ct.Name, "Invalid name.")
      (String.noEmpty ct.Description, "Invalid description.")]