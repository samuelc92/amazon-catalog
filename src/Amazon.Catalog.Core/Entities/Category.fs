namespace Amazon.Catalog.Core.Entities

module Category =
  open System
  
  open Amazon.Catalog.Core
  
  type T = { Id: Guid 
             Name: string
             Description: string
             ParentId: Guid option
             CategoryTypeId: Guid }

  let validate ct=
    Utils.domainValidate ct [
      (String.noEmpty ct.Name, "Invalid name.")
      (String.noEmpty ct.Description, "Invalid description.")]