open System
open Helpers.Testing
open Helpers.Math.Core

let get_sieve_for_x_values x =
    let rec sieve (list : List<bool>) = function
        | n when n > (int (System.Math.Sqrt (float x))) -> list
        | n ->
            match list.Item(n) with
            | true -> sieve (List.mapi (fun index value -> ((index % n <> 0) && value) || (index <= n && value)) list) (n + 1)
            | false -> sieve list (n + 1)
    (sieve (List.init (x + 1) (fun x -> true)) 2).Tail.Tail

let get_primes_up_to x =
    List.rev
        (List.fold2
            (fun acc index value ->
                match value with
                | true -> index :: acc
                | false -> acc)
            []
            [2 .. x]
            (get_sieve_for_x_values x)
        )

let get_rotations x =
    let digits = get_digits x
    let rec loop acc en = function
        | [] -> List.rev acc
        | head :: tail -> loop ((head :: tail @ en) :: acc) (en @ [head]) tail
    (loop [] [] digits) |> List.map (fun list -> (list |> List.map (fun digit -> digit.ToString()) |> List.reduce (+)) |> int)

let primes = get_primes_up_to 999999 |> Set.ofList
let is_circular_prime x = x |> get_rotations |> List.fold (fun is_prime rotation -> is_prime && (primes |> Set.contains rotation)) true

//Tests
run_test "get_primes_up_to 10" [2; 3; 5; 7] (get_primes_up_to 10)
run_test "get_primes_up_to 19" [2; 3; 5; 7; 11; 13; 17; 19] (get_primes_up_to 19)

run_test "get_rotations 1" [1] (get_rotations 1)
run_test "get_rotations 12" [12; 21] (get_rotations 12)
run_test "get_rotations 123" [123; 231; 312] (get_rotations 123)
run_test "get_rotations 123" [1234; 2341; 3412; 4123;] (get_rotations 1234)

run_test "is_circular_prime 2" true (is_circular_prime 2)
run_test "is_circular_prime 6" false (is_circular_prime 6)
run_test "is_circular_prime 7" true (is_circular_prime 7)
run_test "is_circular_prime 11" true (is_circular_prime 11)
run_test "is_circular_prime 12" false (is_circular_prime 12)
run_test "is_circular_prime 13" true (is_circular_prime 13)
run_test "is_circular_prime 19" false (is_circular_prime 19)
run_test "is_circular_prime 197" true (is_circular_prime 197)
run_test "is_circular_prime 519" false (is_circular_prime 519)

let main =
    time_and_save (fun() -> [2 .. 999999] |> List.filter is_circular_prime |> List.length)

[<STAThread>]
do main