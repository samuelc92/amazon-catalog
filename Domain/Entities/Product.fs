namespace Entities

module Product =

  open System

  type T = { Id: Guid 
             Name: string
             Description: string
             Price: decimal
             //Categories: Category.T list
             //Images: Image.T list
             Active: bool }

  let create name description price =
    { T.Id = Guid.NewGuid()
      T.Name=name
      T.Description=description
      T.Price=price
      //T.Categories=categories
      //T.Images=images
      T.Active=true }
     