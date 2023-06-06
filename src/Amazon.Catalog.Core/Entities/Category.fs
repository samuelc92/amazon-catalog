namespace Amazon.Catalog.Core.Entities

module Category =
  
  type T = { Id: int
             Name: string
             Description: string
             CategoryType: CategoryType.T}
             