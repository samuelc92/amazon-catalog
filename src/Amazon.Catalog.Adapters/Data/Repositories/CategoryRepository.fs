namespace Amazon.Catalog.Adapters.Data.Repositories

module CategoryRepository =
  open Donald
  open System.Data

  open Amazon.Catalog.Adapters.Data
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities

  let insert(category: Category.T) =
    Database.conn
    |> Db.newCommand "INSERT INTO public.category VALUES (@Id,@Name,@Description,@ParentId,@CategoryTypeId)"
    |> Db.setParams [
      "@Id", SqlType.Guid category.Id
      "@Name", SqlType.String category.Name
      "@Description", SqlType.String category.Description
      "@ParentId", if Option.isNone category.ParentId then SqlType.Null else SqlType.Guid category.ParentId.Value
      "@CategoryTypeId", SqlType.Guid category.CategoryTypeId
    ]
    |> Db.exec
    |> function
      | Ok _      -> Ok () 
      | Error err -> err |> Helper.convertDbError

  let ofDataReader (rd: IDataReader): Category.T =
    { Id             = rd.ReadGuid       "id"
      Name           = rd.ReadString     "name"
      Description    = rd.ReadString     "description"
      ParentId       = rd.ReadGuidOption "parent_id"
      CategoryTypeId = rd.ReadGuid       "category_type_id" }

  let get limit offset : Result<Category.T list, Error> =
    Database.conn
    |> Db.newCommand "SELECT * FROM public.category LIMIT @Limit OFFSET @Offset  "
    |> Db.setParams [
      "@Limit", SqlType.Int limit
      "@Offset", SqlType.Int offset 
    ]
    |> Db.query ofDataReader
    |> function
      | Ok catTypes -> Ok catTypes 
      | Error err               -> err |> Helper.convertDbError

  let getById id: Result<Category.T option, Error> =
    Database.conn
    |> Db.newCommand "SELECT * FROM public.category WHERE id = @Id "
    |> Db.setParams [
      "@Id", SqlType.Guid id 
    ]
    |> Db.query ofDataReader
    |> function
      | Ok categories->
        match categories with
          | category :: _ -> Ok (Some category)
          | []           -> Ok None
      | Error err               -> err |> Helper.convertDbError