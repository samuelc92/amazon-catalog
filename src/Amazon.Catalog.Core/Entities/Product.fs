namespace Amazon.Catalog.Core.Entities

module Product =

  open System

  type T = { Id: Guid 
             Name: string
             Description: string
             Price: decimal
             //Categories: Category.T list
             //Images: Image.T list
             Active: bool }

  let create prod =
    { prod with Id = Guid.NewGuid(); Active=true }
     