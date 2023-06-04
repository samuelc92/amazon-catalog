module Amazon.Catalog.Helpers

open System.Threading.Tasks

exception InnerError of string

[<RequireQualifiedAccess>]
module Task =
  let bind (f: 'a -> Task<'b>) (x: Task<'a>) = task {
    let! x = x
    return! f x
  }

  let map f = bind (f >> Task.FromResult)

  let ignore (t: Task<_>) = t |> map ignore

module Api =
  let ofResult r =
    match r with
      | Ok a -> a 
      | Error  e -> raise(InnerError("exception test"))