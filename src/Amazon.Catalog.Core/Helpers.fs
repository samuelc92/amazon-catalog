namespace Amazon.Catalog.Core

open System

type Error =
| NotFoundError of string
| DbError of (string * Exception)