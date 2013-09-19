open System
open Helpers.Testing
open Helpers.Math.Core

let generate_lexicographical_permutations list =
    let sorted_list = list |> List.sort
    let rec loop current_choice front (middle :: tail) = seq {
        if tail = [] then
            if front = [] then
                yield current_choice @ [middle]
            else
                yield! loop (current_choice @ [middle]) [] (front @ tail)
        else
            yield! loop (current_choice @ [middle]) [] (front @ tail)
            yield! loop current_choice (front @ [middle]) tail }
    loop [] [] sorted_list |> List.ofSeq

let list_of_digits_to_number list = list |> List.rev |> List.mapi (fun i x -> (i, x)) |> List.fold (fun acc (i, x) -> acc + ((bigint x) * (pown 10I i))) 0I

let get_substrings list = [1 .. (List.length list) - 3] |> List.map (fun x -> (Array.ofList list).[x .. (x + 2)] |> List.ofArray) |> List.map list_of_digits_to_number

run_test "list_of_digits_to_number [1; 2; 3; 4; 5]" 12345I (list_of_digits_to_number [1; 2; 3; 4; 5])

run_test "get_substrings [1; 2; 3; 4; 5]" [234I; 345I] (get_substrings [1; 2; 3; 4; 5])
run_test "get_substrings [1; 2; 3; 4; 5; 6; 7]" [234I; 345I; 456I; 567I] (get_substrings [1; 2; 3; 4; 5; 6; 7])

let primes = [2; 3; 5; 7; 11; 13; 17]
let main =
    time_and_save (fun() -> generate_lexicographical_permutations [0 .. 9]
                            |> List.filter (fun pandigital -> pandigital
                                                              |> get_substrings
                                                              |> List.zip primes
                                                              |> List.map (fun (prime, x) -> x % (bigint prime) = 0I)
                                                              |> List.reduce (&&))
                            |> List.map list_of_digits_to_number
                            |> List.reduce (+))

[<STAThread>]
do main