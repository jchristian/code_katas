open System
open System.Collections.Generic
open Helpers.Testing
open Helpers.Math.Core

let cache = new Dictionary<bigint, list<bigint>>()

let get_prime_factors (x : bigint) =
    if (cache.ContainsKey(x)) then
        (cache.[x])
    else
        let rec loop acc current = function
            | n when (cache.ContainsKey(current)) -> acc @ (cache.[current])
            | n when n > current -> List.rev acc
            | n when current % n = 0I -> loop (n :: acc) (current / n) n
            | n -> loop acc current (n + 1I)
        let f = (loop [] x 2I)
        cache.Add(x, f)
        f

let number_of_prime_factors x = get_prime_factors x |> Set.ofList |> Set.count

let consecutive_integers_with_distinct_prime_factors prime_factors consecutive_number =
    let rec loop (acc, count) = function
        | _ when count = consecutive_number -> acc
        | n ->
            let next = if (number_of_prime_factors n) = prime_factors then (n :: acc, count + 1) else ([], 0)
            loop next (n + 1I)
    loop ([], 0) 2I |> List.rev

run_test "get_prime_factors 2" [2I] (get_prime_factors 2I)
run_test "get_prime_factors 2" [2I] (get_prime_factors 2I)
run_test "get_prime_factors 4" [2I; 2I] (get_prime_factors 4I)
run_test "get_prime_factors 6" [2I; 3I] (get_prime_factors 6I)
run_test "get_prime_factors 12" [2I; 2I; 3I] (get_prime_factors 12I)
run_test "get_prime_factors 14" [2I; 7I] (get_prime_factors 14I)
run_test "get_prime_factors 14" [2I; 7I] (get_prime_factors 14I)
run_test "get_prime_factors 15" [3I; 5I] (get_prime_factors 15I)
run_test "get_prime_factors 23" [23I] (get_prime_factors 23I)

run_test "number_of_prime_factors 5I" 1 (number_of_prime_factors 5I)
run_test "number_of_prime_factors 14I" 2 (number_of_prime_factors 14I)
run_test "number_of_prime_factors 15I" 2 (number_of_prime_factors 15I)
run_test "number_of_prime_factors 644I" 3 (number_of_prime_factors 644I)
run_test "number_of_prime_factors 645I" 3 (number_of_prime_factors 645I)
run_test "number_of_prime_factors 646I" 3 (number_of_prime_factors 646I)

run_test "consecutive_integers_with_distinct_prime_factors 2 2" [14I; 15I] (consecutive_integers_with_distinct_prime_factors 2 2)
run_test "consecutive_integers_with_distinct_prime_factors 3 3" [644I; 645I; 646I] (consecutive_integers_with_distinct_prime_factors 3 3)

let main =
    time_and_save (fun() -> consecutive_integers_with_distinct_prime_factors 4 4)

[<STAThread>]
do main