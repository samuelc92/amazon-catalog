module Amazon.Catalog.Helpers

open System.Threading.Tasks

[<RequireQualifiedAccess>]
module Task =
  let bind (f: 'a -> Task<'b>) (x: Task<'a>) = task {
    let! x = x
    return! f x
  }

  let map f = bind (f >> Task.FromResult)

  let ignore (t: Task<_>) = t |> map ignore