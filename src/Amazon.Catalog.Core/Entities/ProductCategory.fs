namespace Amazon.Catalog.Core.Entities

module ProductCategory =
  open System

  type T = { ProductId: Guid 
             CategoryId: Guid }

  let create (productId: Guid) (categoryId: Guid) =
    { ProductId = productId; CategoryId = categoryId }