open System
open Helpers.Testing
open Helpers.Math.Core

let is_prime n =
  if n <= 1 then false
  elif n = 2 then true
  elif n % 2 = 0 then false
  else seq { 3 .. 2 .. int(sqrt(float(n))) } |> Seq.forall (fun i -> n % i <> 0)

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

let list_of_digits_to_number list = list |> List.mapi (fun i x -> (i, x)) |> List.fold (fun acc (i, x) -> acc + (x * (pown 10 i))) 0

let main =
    time_and_save (fun() -> [for x in 1 .. 9 do yield [1 .. x]] |> Seq.collect generate_lexicographical_permutations |> Seq.map list_of_digits_to_number |> Seq.filter is_prime |> Seq.max)

[<STAThread>]
do main