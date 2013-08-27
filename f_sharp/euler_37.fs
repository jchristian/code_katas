open System
open Helpers.Testing
open Helpers.Math.Core

let rec is_x_divisible_by x = function
    | [] -> false
    | list when list.Head > (int (System.Math.Sqrt (float x))) -> false
    | list when (x % list.Head) = 0 -> true
    | list -> is_x_divisible_by x list.Tail

let mutable primes = []
let mutable highest_prime = 1
let primes_up_to x =
    let rec loop = function
        | cur when cur > x -> ()
        | cur ->
            if not (is_x_divisible_by cur primes) then
                primes <- primes @ [cur]
                highest_prime <- cur
            loop (cur + 1)
    loop (highest_prime + 1)
    primes

let get_root x = int (Math.Sqrt((double x))) + 1

let is_prime x =
    if x < 2 then
        false
    else
        let the_primes = primes_up_to (get_root x)
        is_x_divisible_by x primes |> not

let convert_list_of_digits_to_number list = list |> List.rev |> List.mapi (fun i x -> x * (pown 10 i)) |> List.fold (+) 0

let remove_left_digit x =
    let digits = x |> get_digits
    digits |> List.tail |> convert_list_of_digits_to_number

let remove_right_digit x =
    let digits = x |> get_digits
    digits |> List.rev |> List.tail |> List.rev |> convert_list_of_digits_to_number

let is_truncatably_prime x =
    if x < 10 then
        false
    else
        let rec loop truncate = function
            | 0 -> true
            | x ->
                if x |> is_prime |> not then
                    false
                else
                    loop truncate (truncate x)
        (loop remove_left_digit x) && (loop remove_right_digit x)

run_test "is_prime 0" false (is_prime 0)
run_test "is_prime 1" false (is_prime 1)
run_test "is_prime 2" true (is_prime 2)
run_test "is_prime 4" false (is_prime 4)
run_test "is_prime 7" true (is_prime 7)
run_test "is_prime 103" true (is_prime 103)
run_test "is_prime 117" false (is_prime 117)
run_test "is_prime 118" false (is_prime 118)

run_test "convert_list_of_digits_to_number [1; 2; 3; 4]" 1234 (convert_list_of_digits_to_number [1; 2; 3; 4])

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
                        (potential_match :: (loop potential_match [0 .. 9])) @ (loop cur tail)
                    else if is_prime potential_match then
                        (loop potential_match [0 .. 9]) @ loop cur tail
                    else
                        loop cur tail
            [2; 3; 5; 7] |> List.map (fun elem -> loop elem [0 .. 9]) |> List.reduce (@)
        get_all_truncatably_primes |> List.reduce (+))

[<STAThread>]
do main