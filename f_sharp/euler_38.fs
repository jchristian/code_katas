open System
open Helpers.Testing
open Helpers.Math.Core

let main =
    time_and_save (fun() -> 1)

[<STAThread>]
do main