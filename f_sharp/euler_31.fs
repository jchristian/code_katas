open System
open Helpers.Testing
open Helpers.Math.Core

let number_of_ways_to_make x values =
    let rec loop remaining = function
        | values when remaining = 0 -> 1
        | values when remaining < 0 -> 0
        | [] -> 0
        | head :: tail ->
            let bredth = loop (remaining - head) (head :: tail)
            let depth = loop remaining tail
            bredth + depth
    loop x values

let british_currency = [1; 2; 5; 10; 20; 50; 100; 200]

run_test "number_of_ways_to_make 5 british_currency" 4 (number_of_ways_to_make 5 british_currency)
run_test "number_of_ways_to_make 10 british_currency" 11 (number_of_ways_to_make 10 british_currency)

let main =
    time_and_save (fun() -> number_of_ways_to_make 200 british_currency)

[<STAThread>]
do main