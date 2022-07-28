module Program

open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Giraffe
open HttpHandlers

let webApp = 
    choose [
        subRoute "/api"
            (choose [
                GET >=> choose [
                    route "/hello" >=> handlerGetHello
                ]
            ])
        setStatusCode 404 >=> text "Not Found"
    ]

let errorHander (ex: Exception) (logger: ILogger) =
    logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message
 
let args = Environment.GetCommandLineArgs()
let builder = WebApplication.CreateBuilder(args)
builder.Services.AddGiraffe() |> ignore

let app = builder.Build()
app.UseGiraffe(webApp)
app.Run()

exit 0