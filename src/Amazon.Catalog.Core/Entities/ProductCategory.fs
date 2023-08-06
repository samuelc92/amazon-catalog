namespace Amazon.Catalog.Core.Entities

module ProductCategory =
  open System

  type T = { ProductId: Guid 
             CategoryId: Guid }