open System
open Helpers.Testing
open Helpers.Math.Core

let memoized_factorial = memoize factorial

let sum_of_factorialed_digits x = x |> get_digits |> List.map memoized_factorial |> List.reduce (+)

let value_equal_to_sum_of_factorialzed_digits = [10I .. 2500000I] |> List.filter (fun x -> (sum_of_factorialed_digits x) = x)

run_test "sum_of_factorialed_digits 123" (9I) (sum_of_factorialed_digits 123)
run_test "sum_of_factorialed_digits 1234" (33I) (sum_of_factorialed_digits 1234)
run_test "sum_of_factorialed_digits 224" (28I) (sum_of_factorialed_digits 224)

//printfn "%A" (value_equal_to_sum_of_factorialzed_digits)

let main =
    time_and_save (fun() -> value_equal_to_sum_of_factorialzed_digits |> List.reduce (+))

[<STAThread>]
do main