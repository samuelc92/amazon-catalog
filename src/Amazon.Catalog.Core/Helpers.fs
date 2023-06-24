namespace Amazon.Catalog.Core

open System

type Error =
| NotFoundError of string
| DbError of (string * Exception)
| DomainError of string
| DomainErrors of string list

module String =
  let inline empty str = String.IsNullOrWhiteSpace(str)
  let inline noEmpty str = not(empty str) 