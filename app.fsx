#r @"packages/Suave/lib/net40/Suave.dll"
#r @"packages/FAKE/tools/FakeLib.dll"

open System
open System.IO

open Fake
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



Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
 
let app =
  let root = IO.Path.Combine(__SOURCE_DIRECTORY__, "web")
  choose
        [ GET >=> choose
            [ path "/" >=> Files.browseFile root "index.html"
              path "/goodbye" >=> OK "Good bye GET" ]
        ]  

//Compose & start the web server!

Target "run" (fun _ ->
                let cts = new CancellationTokenSource()
                let conf = { defaultConfig with cancellationToken = cts.Token }
                let listening, server = startWebServerAsync conf app
                Async.Start(server, cts.Token)
                printfn "Make requests now"
                Console.ReadKey true |> ignore
                cts.Cancel())

RunTargetOrDefault "run"