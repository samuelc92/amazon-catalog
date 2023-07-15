namespace Amazon.Catalog.Adapters.Data.Repositories

module CategoryRepository =
  open Donald

  open Amazon.Catalog.Adapters.Data
  open Amazon.Catalog.Core
  open Amazon.Catalog.Core.Entities

  let insert(category: Category.T) =
    Database.conn
    |> Db.newCommand "INSERT INTO public.category VALUES (@Id,@Name,@Description,@CategoryId,@CategoryTypeId)"
    |> Db.setParams [
      "@Id", SqlType.Guid category.Id
      "@Name", SqlType.String category.Name
      "@Description", SqlType.String category.Description
      "@CategoryId", if Option.isNone category.ParentId then SqlType.Null else SqlType.Guid category.ParentId.Value
      "@CategoryTypeId", SqlType.Guid category.CategoryTypeId
    ]
    |> Db.exec
    |> function
      | Ok _      -> Ok () 
      | Error err -> err |> Helper.convertDbError
