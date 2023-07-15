namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module CreateCategoryCommand =
  open System
   
  open Amazon.Catalog.Adapters.Data.Repositories
  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.Core.Utils
   
  type Input = { Name: string
                 Description: string
                 ParentId: Guid option
                 CategoryTypeId: Guid }

  let createEntity input: Category.T =
    { Id = Guid.NewGuid()
      Name=input.Name
      Description=input.Description
      ParentId=input.ParentId
      CategoryTypeId=input.CategoryTypeId }

  let handle (req: Input) =
    createEntity req
    |> Category.validate
    >>= CategoryRepository.insert