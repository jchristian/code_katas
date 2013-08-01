open System
open Helpers.Testing
open Helpers.Math.Core

let add_multiply = (fun x y -> x() + y) >> (*)

//printfn "%A" (add_multiply 2 3)
printfn "%A" (add_multiply)