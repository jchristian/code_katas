open System
open System.Collections.Generic
open Helpers.Testing
open Helpers.Math.Core

let main =
    time_and_save (fun() -> [1 .. 1000]
                            |> List.map (fun x -> pown (bigint x) x)
                            |> List.reduce (+)
                            |> (fun x -> (x.ToString()))
                            |> (fun x -> x.[x.Length - 10 .. x.Length - 1]))

[<STAThread>]
do main