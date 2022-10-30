namespace Hw6

type Message =
    | SuccessfulExecution of string
    | WrongArgLength of string
    | WrongArgFormat of string
    | WrongArgFormatOperation of string
    | DivideByZero