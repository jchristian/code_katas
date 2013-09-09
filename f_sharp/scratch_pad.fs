open System
open Helpers.Testing
open Helpers.Math.Core

let ip f x = fun y -> f x y

let test = ip (fun x y -> x * 2 + y ) 5

printfn "%A" (test 1)