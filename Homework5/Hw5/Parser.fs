module Hw5.Parser

open System
open Hw5.Calculator

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    match args.Length with
    | 3 -> Ok args
    | _ -> Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Calculator.plus -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | Calculator.minus -> Ok (arg1, CalculatorOperation.Minus, arg2)
    | Calculator.multiply -> Ok (arg1, CalculatorOperation.Multiply, arg2)
    | Calculator.divide -> Ok (arg1, CalculatorOperation.Divide, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    match Double.TryParse(args[0]) with
    | true, arg1 -> match Double.TryParse(args[2]) with
        | true, arg2 -> isOperationSupported (arg1, args[1], arg2)
        | _ -> Error Message.WrongArgFormat
    | _ -> Error Message.WrongArgFormat

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation, arg2 with
    | CalculatorOperation.Divide, 0.0 -> Error Message.DivideByZero
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe
        {
            let! lengthSupportArgs = args |> isArgLengthSupported
            let! parsedArgs = lengthSupportArgs |> parseArgs
            let! divideByZeroCheck = parsedArgs |> isDividingByZero
            return divideByZeroCheck
        } 