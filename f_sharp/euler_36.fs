open System
open Helpers.Testing
open Helpers.Math.Core

let to_base_two (x : int) = Convert.ToString(x, 2)

let are_base_two_and_ten_are_palindromes x =
    is_palindrome (x.ToString()) && is_palindrome (to_base_two x)

run_test "to_base_two 585" "1001001001" (to_base_two 585)
run_test "to_base_two 1" "1" (to_base_two 1)
run_test "to_base_two 2" "10" (to_base_two 2)

run_test "are_base_two_and_ten_are_palindromes 1" true (are_base_two_and_ten_are_palindromes 1)
run_test "are_base_two_and_ten_are_palindromes 2" false (are_base_two_and_ten_are_palindromes 2)
run_test "are_base_two_and_ten_are_palindromes 3" true (are_base_two_and_ten_are_palindromes 3)
run_test "are_base_two_and_ten_are_palindromes 10" false (are_base_two_and_ten_are_palindromes 10)
run_test "are_base_two_and_ten_are_palindromes 99" true (are_base_two_and_ten_are_palindromes 99)
run_test "are_base_two_and_ten_are_palindromes 585" true (are_base_two_and_ten_are_palindromes 585)

let main =
    time_and_save (fun() -> [1 .. 999999] |> List.filter are_base_two_and_ten_are_palindromes |> List.map bigint |> List.reduce (+))

[<STAThread>]
do main