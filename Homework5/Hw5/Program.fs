module Hw5.Program

open System
open Hw5

let convertMessage (message: Message) =
    match message with
    | Message.SuccessfulExecution -> "Успешное выполнение"
    | Message.WrongArgLength -> "Неверное количество данных - ожидались 2 аргумента и 1 операция в последовательности: 1-й аргумент, операция, 2-й аргумент"
    | Message.WrongArgFormat -> "Неверный формат аргументов, допустимое значение: число"
    | Message.WrongArgFormatOperation -> "Неверный формат операции, допустимы: + - * /"
    | Message.DivideByZero -> "Недопустимая операция - деление 1-го аргумента на ноль"

let successResult (arg1, operation, arg2) =
    printfn $"{convertMessage Message.SuccessfulExecution}, результат = {Calculator.calculate arg1 operation arg2}"
    0
    
let failureResult message =
    printfn $"{convertMessage message}"
    -1

[<EntryPoint>]
let main (args: string[]) =
    match Parser.parseCalcArguments args with
    | Ok parsedArgs -> successResult parsedArgs
    | Error message -> failureResult message