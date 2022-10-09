module Hw4.Parser

open System
open System.Diagnostics.CodeAnalysis
open Hw4.Calculator

[<ExcludeFromCodeCoverage>]
type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    args.Length = 3

let parseOperation (arg: string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ ->  InvalidOperationException() |> raise
    
let tryParse (s: string) =
    match System.Double.TryParse(s) with
    | true, value -> value
    | _ -> ArgumentException() |> raise

let parseCalcArguments(args : string[]) =
    if(not (isArgLengthSupported args)) then
        ArgumentException() |> raise
    let val1 = tryParse args[0]
    let val2 = tryParse args[2]
    let operation = parseOperation args[1]
    {arg1 = val1; arg2 = val2; operation = operation}

