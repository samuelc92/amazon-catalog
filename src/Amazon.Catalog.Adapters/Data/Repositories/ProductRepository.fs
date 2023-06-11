namespace Amazon.Catalog.Adapters.Data.Repositories

module ProductRepository =
  open Donald
  open System.Data

  open Amazon.Catalog.Adapters.Data
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities

  let insert(prod: Product.T) =
    Database.conn
    |> Db.newCommand "INSERT INTO public.product VALUES (@Id,@Name,@Description,@Price,@Active)"
    |> Db.setParams [
      "@Id", SqlType.Guid prod.Id
      "@Name", SqlType.String prod.Name
      "@Description", SqlType.String prod.Description
      "@Price", SqlType.Decimal prod.Price
      "@Active", SqlType.Boolean prod.Active
    ]
    |> Db.exec
    |> function
      | Ok _      -> Ok prod
      | Error err -> err |> Helper.convertDbError
  
  let ofDataReader (rd: IDataReader): Product.T =
    { Id          = rd.ReadGuid "Id"
      Name        = rd.ReadString "Name"
      Description = rd.ReadString "Description"
      Price       = rd.ReadDecimal "Price"
      Active      = rd.ReadBoolean "Active" }
    
  let get: Result<Product.T list, Error> =
    Database.conn
    |> Db.newCommand "SELECT * FROM public.product"
    |> Db.query ofDataReader
    |> function
      | Ok prods -> Ok prods 
      | Error err       -> err |> Helper.convertDbError

  let getById id =
    Database.conn
    |> Db.newCommand "SELECT * FROM public.product WHERE \"Id\"=@Id"
    |> Db.setParams [
      "@Id", SqlType.Guid id
    ]
    |> Db.query ofDataReader
    |> function
      | Ok prods ->
        match prods with
          | prod :: _ -> Ok prod 
          | []        -> Error (NotFoundError "Product not found")
      | Error err       -> err |> Helper.convertDbError