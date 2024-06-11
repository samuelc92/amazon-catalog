namespace Amazon.Catalog.Adapters.Dapr
    
[<RequireQualifiedAccess>]
module Queue =
  open Dapr.Client
  open Serilog

  open Amazon.Catalog.Core.Entities

  let daprClient = DaprClientBuilder().Build()

  let PublishEventAsync (product: Product.T) =
    Log.Logger.Information "Publishing product event"
    async {
        daprClient.PublishEventAsync("pubsub", "products", product)
        |> Async.AwaitTask
        |> ignore
    }
    |> Async.RunSynchronously
    |> ignore 
    Ok product
