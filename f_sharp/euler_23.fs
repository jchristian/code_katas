open System
open Helpers.Testing
open Helpers.Math.Core

let is_abundant x =
    let sum_of_proper_divisors = x |> get_proper_divisors |> List.reduce (+)
    sum_of_proper_divisors > x

printfn "Generating abundant numbers..."
let abundant_numbers = [1I .. 28123I] |> List.filter is_abundant
let abundant_numbers_array = abundant_numbers |> List.toArray
printfn "Finished generating abundant numbers"

let is_sum_of_two_abundant_numbers n =
    let rec loop = function
        | x1 :: outer_tail when x1 > n -> false
        | x1 :: outer_tail ->
            let is_sum = System.Array.BinarySearch(abundant_numbers_array, (n - x1)) >= 0
            if is_sum then
                true
            else
                loop outer_tail
    loop abundant_numbers

run_test "is_abundant 1" false (is_abundant 1I)
run_test "is_abundant 2" false (is_abundant 2I)
run_test "is_abundant 3" false (is_abundant 3I)
run_test "is_abundant 4" false (is_abundant 4I)
run_test "is_abundant 10" false (is_abundant 10I)
run_test "is_abundant 12" true (is_abundant 12I)

run_test "is_sum_of_two_abundant_numbers 1" false (is_sum_of_two_abundant_numbers 1I)
run_test "is_sum_of_two_abundant_numbers 3" false (is_sum_of_two_abundant_numbers 3I)
run_test "is_sum_of_two_abundant_numbers 4" false (is_sum_of_two_abundant_numbers 4I)
run_test "is_sum_of_two_abundant_numbers 12" false (is_sum_of_two_abundant_numbers 12I)
run_test "is_sum_of_two_abundant_numbers 18" false (is_sum_of_two_abundant_numbers 18I)
run_test "is_sum_of_two_abundant_numbers 24" true (is_sum_of_two_abundant_numbers 24I)
run_test "is_sum_of_two_abundant_numbers 32" true (is_sum_of_two_abundant_numbers 32I)
run_test "is_sum_of_two_abundant_numbers 28123" true (is_sum_of_two_abundant_numbers 28123I)

let main =
    time_and_save (fun() -> [1I .. 28123I] |> List.filter (fun x -> not (is_sum_of_two_abundant_numbers x)) |> List.reduce (+))

[<STAThread>]
do main