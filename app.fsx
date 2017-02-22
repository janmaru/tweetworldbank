#r "packages/Suave/lib/net40/Suave.dll"

open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

open System
open System.Web
open System.Text
open System.Collections.Generic
open FSharp.Data
 
 
open System
open System.Web
open System.Text
open System.Collections.Generic
open FSharp.Data

open Suave
open Suave.Web
open Suave.Http
open Suave.Filters
open Suave.Operators
open Suave.Sockets
open Suave.Sockets.Control
open Suave.Sockets.AsyncSocket
open Suave.WebSocket
open Suave.Utils
open System.Threading
open Suave.Successful

//Compose & start the web server!
let app =

    // Compose & start the web server!
    let part =
      let root = IO.Path.Combine(__SOURCE_DIRECTORY__, "web")
      choose 
        [ 
//          path "/maptweets" >=> handShake (socketOfObservable mapTweets)
//          path "/feedtweets" >=> handShake (socketOfObservable feedTweets)
//          path "/frequencies" >=> handShake (socketOfObservable phraseUpdates)
//          path "/zones" >=> Successful.OK timeZonesJson
          path "/" >=> Files.browseFile root "index.html" 
          Files.browse root ]

    let cts = new CancellationTokenSource()
    let conf = { defaultConfig with cancellationToken = cts.Token }
    let listening, server = startWebServerAsync conf part
    Async.Start(server, cts.Token)
    printfn "Make requests now"
    Console.ReadKey true |> ignore
    cts.Cancel()