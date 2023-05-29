namespace Entities

module Product =

  type T = { Id: int;
             Name: string;
             Description: string;
             Price: decimal }

  let create name description price =
    { T.Id=0;
      T.Name=name;
      T.Description=description;
      T.Price=price }
     