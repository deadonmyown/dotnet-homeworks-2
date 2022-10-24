module Hw6.App

open System
open System.Collections.Generic
open System.Diagnostics.CodeAnalysis
open System.Threading
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Hosting.Internal
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Hw6

[<ExcludeFromCodeCoverage>]
let getCalculatorResult (query: string[]) =
    match Parser.parseCalcArguments query with
    | Ok (arg1, operation, arg2) -> Ok $"{Calculator.calculate arg1 operation arg2}"
    | Error DivideByZero -> Ok $"{DivideByZero}"
    | Error message -> match message with
                       | WrongArgFormat arg | WrongArgFormatOperation arg -> Error $"Could not parse value '{arg}'"
                       | WrongArgLength arg -> Error $"Invalid amount of data : expected 3 arguments, was : {arg} arguments"
                       | _ -> Error "Unhandled error"
    
let getQueryParams (ctx: HttpContext) =
     ctx.Request.Query.Keys |>  Seq.cast |> Array.ofSeq |> Array.map(fun (k: string) -> string(ctx.Request.Query.Item k).ToLower())
    
let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> = ctx |> getQueryParams |> getCalculatorResult
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

[<ExcludeFromCodeCoverage>]
let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "Use //calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
             route "/calculate" >=> calculatorHandler
        ]
        setStatusCode 404 >=> text "Not Found"
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseGiraffe webApp
        
[<ExcludeFromCodeCoverage>]
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()        
    0