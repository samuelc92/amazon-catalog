namespace Amazon.Catalog.Application.Comands

[<RequireQualifiedAccess>]
module CreateCategoryTypeCommand =
  open System
   
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities
  open Amazon.Catalog.Adapters.Data.Repositories
   
  type Input = { Name: string
                 Description: string
                 ShowOnMenu: bool
                 ShowOnHome: bool }

  let createEntity input: CategoryType.T =
    { Id = Guid.NewGuid()
      Name=input.Name
      Description=input.Description
      ShowOnMenu=input.ShowOnMenu
      ShowOnHome=input.ShowOnHome }

  let handle (req: Input) =
    createEntity req
    |> CategoryType.validate