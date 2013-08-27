open System
open Helpers.Testing
open Helpers.Math.Core

let get_root x = int (Math.Sqrt((double x)))

let is_prime n =
  if n <= 1 then false
  elif n = 2 then true
  elif n % 2 = 0 then false
  else seq { 3 .. 2 .. int(sqrt(float(n))) } |> Seq.forall (fun i -> n % i <> 0)

let memoized_is_prime = memoize is_prime

let to_int_zero_if_empty s = if Seq.length s = 0 then 0 else int s
let remove_left_digit x = (x.ToString()) |> (fun s -> s.[1 ..]) |> to_int_zero_if_empty
let remove_right_digit x = (x.ToString()) |> (fun s -> s.[..(Seq.length s - 2)]) |> to_int_zero_if_empty

let is_truncatably_prime x =
    if x < 10 then
        false
    else
        let rec loop truncate = function
            | 0 -> true
            | x ->
                if x |> memoized_is_prime |> not then
                    false
                else
                    loop truncate (truncate x)
        (loop remove_left_digit x) && (loop remove_right_digit x)

run_test "is_prime 0" false (is_prime 0)
run_test "is_prime 1" false (is_prime 1)
run_test "is_prime 2" true (is_prime 2)
run_test "is_prime 4" false (is_prime 4)
run_test "is_prime 7" true (is_prime 7)
run_test "is_prime 37" true (is_prime 37)
run_test "is_prime 73" true (is_prime 73)
run_test "is_prime 103" true (is_prime 103)
run_test "is_prime 117" false (is_prime 117)
run_test "is_prime 118" false (is_prime 118)

run_test "remove_left_digit 1234" 234 (remove_left_digit 1234)
run_test "remove_right_digit 1234" 123 (remove_right_digit 1234)

run_test "is_truncatably_prime 7" false (is_truncatably_prime 7)
run_test "is_truncatably_prime 27" false (is_truncatably_prime 27)
run_test "is_truncatably_prime 37" true (is_truncatably_prime 37)
run_test "is_truncatably_prime 67" false (is_truncatably_prime 67)
run_test "is_truncatably_prime 379" false (is_truncatably_prime 379)
run_test "is_truncatably_prime 3797" true (is_truncatably_prime 3797)
run_test "is_truncatably_prime 37973" false (is_truncatably_prime 37973)
run_test "is_truncatably_prime 739397" true (is_truncatably_prime 739397)

let main =
    time_and_save (fun() ->
        let get_all_truncatably_primes =
            let rec loop cur = function
                | [] -> []
                | head :: tail ->
                    let potential_match = cur * 10 + head
                    if is_truncatably_prime potential_match then
                        (potential_match :: (loop potential_match [1; 3; 7; 9])) @ (loop cur tail)
                    else if memoized_is_prime potential_match then
                        (loop potential_match  [1; 3; 7; 9]) @ loop cur tail
                    else
                        loop cur tail
            [2; 3; 5; 7] |> List.map (fun elem -> loop elem  [1; 3; 7; 9]) |> List.reduce (@)
        get_all_truncatably_primes |> List.reduce (+))

[<STAThread>]
do main