namespace Amazon.Catalog.Adapters.Dapr
    
[<RequireQualifiedAccess>]
module Queue =
  open Dapr.Client
  open Microsoft.Extensions.Logging
  open Serilog

  open Amazon.Catalog.Core.Entities

  let daprClient = DaprClientBuilder().Build()

  let PublishEventAsync (product: Product.T) =
    Log.Logger.Information "Publishing product event"
    async {
        daprClient.PublishEventAsync("productpubsub", "products", product)
        |> Async.AwaitTask
        |> ignore
    }
    |> Async.RunSynchronously
    |> ignore 
    Ok product
