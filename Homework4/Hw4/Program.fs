module Hw4.Program

open System
open Hw4

let throwExc (exc: Exception) =
    printfn $"{exc.Message}"
    -1

[<EntryPoint>]
let main (args: string[]) =
    try
        let parsedData = Parser.parseCalcArguments args
        printfn $"{Calculator.calculate parsedData.arg1 parsedData.operation parsedData.arg2}"
        0
    with
        | :? ArgumentException as exc -> throwExc exc
        | :? InvalidOperationException as exc -> throwExc exc

        