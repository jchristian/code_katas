open System
open Helpers.Testing
open Helpers.Math.Core

let spiral_diagonals x =
    let rec loop acc cur = function
        | step when step > x -> acc
        | step -> loop (acc @ ([1 .. 4] |> List.map (fun y -> cur + (y * step)))) (cur + step * 4) (step + 2)
    loop [1] 1 2


run_test "spiral_diagonals 3" [1; 3; 5; 7; 9] (spiral_diagonals 3)
run_test "spiral_diagonals 5" [1; 3; 5; 7; 9; 13; 17; 21; 25] (spiral_diagonals 5)

let main =
    time_and_save (fun() -> spiral_diagonals 1001 |> List.reduce (+))

[<STAThread>]
do main