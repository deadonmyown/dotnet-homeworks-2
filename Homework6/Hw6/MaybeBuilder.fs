﻿module Hw6.MaybeBuilder

open System

type MaybeBuilder() =
    member builder.Bind(a, f): Result<'e,'d> =
        match a with
        | Ok res -> f res
        | Error message -> Error message
    member builder.Return x: Result<'a,'b> = Ok x
let maybe = MaybeBuilder()