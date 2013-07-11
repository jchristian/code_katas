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
    loop [] [] sorted_list

run_test "generate_lexicographical_permutations [0; 1; 2]" [[0; 1; 2]; [0; 2; 1]; [1; 0; 2]; [1; 2; 0]; [2; 0; 1]; [2; 1; 0]] (generate_lexicographical_permutations [0; 1; 2] |> Seq.toList)


let main =
    time_and_save (fun() -> generate_lexicographical_permutations [0 .. 9] |> Seq.nth 999999 |> List.fold (fun acc x -> acc + (x.ToString())) "")

[<STAThread>]
do main