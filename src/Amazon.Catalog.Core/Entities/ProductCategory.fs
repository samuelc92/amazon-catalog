namespace Amazon.Catalog.Core.Entities

module ProductCategory =
  open System

  open Amazon.Catalog.Core

  type T = { ProductId: Guid 
             CategoryId: Guid }