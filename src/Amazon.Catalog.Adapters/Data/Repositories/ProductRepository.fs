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

  let delete id =
    Database.conn
    |> Db.newCommand "DELETE FROM public.product WHERE \"Id\"=@Id"
    |> Db.setParams [ "@Id" , SqlType.Guid id]
    |> Db.exec
    |> function
      | Ok _ -> Ok ()
      | Error err -> err |> Helper.convertDbError
  
  let update (product: Product.T)=
    Database.conn
    |> Db.newCommand "UPDATE public.product SET \"Name\"=@Name,\"Description\"=@Description,\"Price\"=@Price WHERE \"Id\"=@Id"
    |> Db.setParams [
      "@Id",          SqlType.Guid    product.Id 
      "@Name",        SqlType.String  product.Name
      "@Description", SqlType.String  product.Description
      "@Price",       SqlType.Decimal product.Price 
    ]
    |> Db.exec
    |> function
      | Ok _ -> Ok ()
      | Error err -> err |> Helper.convertDbError

  let ofDataReader (rd: IDataReader): Product.T =
    { Id          = rd.ReadGuid "Id"
      Name        = rd.ReadString "Name"
      Description = rd.ReadString "Description"
      Price       = rd.ReadDecimal "Price"
      Active      = rd.ReadBoolean "Active" }
    
  let get limit offset : Result<Product.T list, Error> =
    Database.conn
    |> Db.newCommand "SELECT * FROM public.product LIMIT @Limit OFFSET @Offset  "
    |> Db.setParams [
      "@Limit", SqlType.Int limit
      "@Offset", SqlType.Int offset 
    ]
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

  let getByName name =
    Database.conn
    |> Db.newCommand "SELECT * FROM public.product WHERE \"Name\"=@Name"
    |> Db.setParams [
      "@Name", SqlType.String name 
    ]
    |> Db.query ofDataReader
    |> function
      | Ok prods ->
        match prods with
          | prod :: _ -> Ok (Some prod) 
          | []        -> Ok None
      | Error err       -> err |> Helper.convertDbError