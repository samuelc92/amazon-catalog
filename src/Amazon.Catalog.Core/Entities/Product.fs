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

  let create name description price=
    { Id = Guid.NewGuid(); Name=name; Description=description; Price=price; Active=true }
     