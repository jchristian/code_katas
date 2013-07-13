open System
open Helpers.Testing
open Helpers.Math.Core

let recurring_cycle_size x =
    let rec loop acc = function
        | 0 -> 0
        | remainder ->
            if acc |> List.exists (fun y -> y = remainder) then
                acc |> List.findIndex (fun y -> y = remainder) |> (fun x -> x + 1)
            else
                let current = (remainder * 10) / x
                let next_remainder = (remainder * 10) % x
                loop (remainder :: acc) next_remainder
    loop [] 1

run_test "recurring_cycle_size 2" 0 (recurring_cycle_size 2)
run_test "recurring_cycle_size 3" 1 (recurring_cycle_size 3)
run_test "recurring_cycle_size 4" 0 (recurring_cycle_size 4)
run_test "recurring_cycle_size 6" 1 (recurring_cycle_size 6)
run_test "recurring_cycle_size 7" 6 (recurring_cycle_size 7)
run_test "recurring_cycle_size 11" 2 (recurring_cycle_size 11)
run_test "recurring_cycle_size 13" 6 (recurring_cycle_size 13)
run_test "recurring_cycle_size 81" 9 (recurring_cycle_size 81)

let main =
    time_and_save (fun() -> [2 .. 1000] |> List.map (fun x -> (recurring_cycle_size x, x)) |> List.sortBy (fun (s, i) -> s) |> List.rev |> List.head |> snd)

[<STAThread>]
do main