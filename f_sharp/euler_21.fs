open System
open Helpers.Testing
open Helpers.Math.Core

let get_sum_of_proper_divisors x = (get_proper_divisors x) |> List.fold (+) 0I

let is_amicable_pair a =
    if a = 0I then
        false
    else
        let da = get_sum_of_proper_divisors a
        let b = da
        let db = get_sum_of_proper_divisors b
        da = b && db = a && a <> b

run_test "is_amicable_pair 1" false (is_amicable_pair 1I)
run_test "is_amicable_pair 2" false (is_amicable_pair 2I)
run_test "is_amicable_pair 4" false (is_amicable_pair 4I)
run_test "is_amicable_pair 6" false (is_amicable_pair 6I)
run_test "is_amicable_pair 220" true (is_amicable_pair 220I)

let main =
    time_and_save (fun() -> [1I .. 10000I] |> List.filter is_amicable_pair |> List.reduce (+))

[<STAThread>]
do main
