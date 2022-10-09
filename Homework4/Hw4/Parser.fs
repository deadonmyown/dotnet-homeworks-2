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
    match isArgLengthSupported args with
    | true -> {
              arg1 = tryParse args[0];
              arg2 = tryParse args[2];
              operation = parseOperation args[1]
              }
    | false -> ArgumentException() |> raise
    

