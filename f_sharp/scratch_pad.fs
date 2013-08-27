open System
open Helpers.Testing
open Helpers.Math.Core

let add x y = 100 * x + y
let add2 = add 2

printfn "%A" ((20 |> add) 2)