namespace Amazon.Catalog.Adapters.Data.Repositories

module CategoryTypeRepository =
  open Donald

  open Amazon.Catalog.Adapters.Data
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