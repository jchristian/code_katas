open System
open Helpers.Testing
open Helpers.Math.Core

let distinct_combinations =
    let rec outer_loop acc = function
        | 101 -> acc
        | x ->
            let rec inner_loop acc = function
                | 101 -> outer_loop acc (x + 1)
                | y -> inner_loop (acc |> Set.add (pown (bigint x) y)) (y + 1)
            inner_loop acc 2
    outer_loop Set.empty 2

let main =
    time_and_save (fun() -> (Set.count (distinct_combinations)))

[<STAThread>]
do main