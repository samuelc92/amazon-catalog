namespace Entities

module Product =

  type T = { Id: int
             Name: string
             Description: string
             Price: decimal
             //Categories: Category.T list
             //Images: Image.T list
             Active: bool }

  let create name description price =
    { T.Id=0
      T.Name=name
      T.Description=description
      T.Price=price
      //T.Categories=categories
      //T.Images=images
      Active=true }
     