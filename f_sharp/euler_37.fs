open System
open Helpers.Testing
open Helpers.Math.Core

let is_truncatably_prime x = true

let rec is_x_divisible_by x = function
    | [] -> false
    | list when list.Head > (int (System.Math.Sqrt (float x))) -> false
    | list when (x % list.Head) = 0 -> true
    | list -> is_x_divisible_by x list.Tail

let primes = new System.Collections.Generic.HashSet<int>()
let mutable highest_prime = 1
let primes_up_to x =
    let rec loop = function
        | cur when cur > x -> ()
        | cur ->
            if not (is_x_divisible_by cur (List.ofSeq primes)) then
                primes.Add(cur)
                highest_prime <- cur
            loop (cur + 1)
    loop (highest_prime + 1)
    primes

let get_root x = int (Math.Sqrt((double x))) + 1

let is_prime x =
    let ths_primes = primes_up_to x
    ths_primes.Contains(x)

run_test "is_prime 2" true (is_prime 2)
run_test "is_prime 4" false (is_prime 4)
run_test "is_prime 7" true (is_prime 7)
run_test "is_prime 103" true (is_prime 103)
run_test "is_prime 117" false (is_prime 117)
run_test "is_prime 118" false (is_prime 118)

run_test "is_truncatably_prime 7" false (is_truncatably_prime 7)
run_test "is_truncatably_prime 27" false (is_truncatably_prime 27)
run_test "is_truncatably_prime 37" true (is_truncatably_prime 37)
run_test "is_truncatably_prime 67" false (is_truncatably_prime 67)
run_test "is_truncatably_prime 379" false (is_truncatably_prime 379)
run_test "is_truncatably_prime 3797" true (is_truncatably_prime 3797)

let main =
    time_and_save (fun() -> 1)

[<STAThread>]
do main