namespace Amazon.Catalog.Adapters.Data.Repositories

module CategoryTypeRepository =
  open Donald
  open System.Data

  open Amazon.Catalog.Adapters.Data
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities

  let insert(catType: CategoryType.T) =
    Database.conn
    |> Db.newCommand "INSERT INTO public.category_type VALUES (@Id,@Name,@Description,@ShowOnMenu,@ShowOnHome)"
    |> Db.setParams [
      "@Id", SqlType.Guid catType.Id
      "@Name", SqlType.String catType.Name
      "@Description", SqlType.String catType.Description
      "@ShowOnMenu", SqlType.Boolean catType.ShowOnMenu
      "@ShowOnHome", SqlType.Boolean catType.ShowOnHome
    ]
    |> Db.exec
    |> function
      | Ok _      -> Ok () 
      | Error err -> err |> Helper.convertDbError

  let ofDataReader (rd: IDataReader): CategoryType.T =
    { Id          = rd.ReadGuid    "id"
      Name        = rd.ReadString  "name"
      Description = rd.ReadString  "description"
      ShowOnMenu  = rd.ReadBoolean "show_on_menu"
      ShowOnHome  = rd.ReadBoolean "show_on_home" }

  let get limit offset : Result<CategoryType.T list, Error> =
    Database.conn
    |> Db.newCommand "SELECT * FROM public.category_type LIMIT @Limit OFFSET @Offset  "
    |> Db.setParams [
      "@Limit", SqlType.Int limit
      "@Offset", SqlType.Int offset 
    ]
    |> Db.query ofDataReader
    |> function
      | Ok catTypes -> Ok catTypes 
      | Error err               -> err |> Helper.convertDbError
