open System
open Helpers.Testing
open Helpers.Math.Core

let rec number_of_ways_to_make x = function
    | values when x = 0 -> 1
    | values when x < 0 -> 0
    | [] -> 0
    | head :: tail ->
        let bredth = number_of_ways_to_make (x - head) (head :: tail)
        let depth = number_of_ways_to_make x tail
        bredth + depth

let british_currency = [200; 100; 50; 20; 10; 5; 2; 1]

run_test "number_of_ways_to_make 5 british_currency" 4 (number_of_ways_to_make 5 british_currency)
run_test "number_of_ways_to_make 10 british_currency" 11 (number_of_ways_to_make 10 british_currency)

let main =
    time_and_save (fun() -> (memoize number_of_ways_to_make) 200 british_currency)
    time_and_save (fun() -> number_of_ways_to_make 200 british_currency)

[<STAThread>]
do main