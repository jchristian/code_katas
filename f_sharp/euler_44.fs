open System
open Helpers.Testing
open Helpers.Math.Core

let pentagon_number x = x * (3 * x - 1) / 2

let pentagon_numbers =
  let rec pentagon_numbers_after x = seq { yield pentagon_number x; yield! pentagon_numbers_after (x + 1) }
  pentagon_numbers_after 1

let pentagonal_index x = (1.0 + (Math.Sqrt((float x) * 24.0 + 1.0))) / 6.0
let is_pentagonal x = (pentagonal_index x) = (float (int (pentagonal_index x)))
let are_sum_and_difference_pentagonal (pi, pj) = printfn "%A %A %A %A" pi pj (pj - pi) (pi + pj); is_pentagonal (pj - pi) && is_pentagonal (pi + pj)

let pentagonal_pairs_with_difference_of x =
    pentagon_numbers |> Seq.takeWhile ((>) (x / 2)) |> Seq.choose (fun p -> if is_pentagonal (x - p) then Some (p, (x - p)) else None)

run_test "is_pentagonal 1" true (is_pentagonal 1)
run_test "is_pentagonal 5" true (is_pentagonal 5)
run_test "is_pentagonal 50" false (is_pentagonal 50)
run_test "is_pentagonal 51" true (is_pentagonal 51)
run_test "is_pentagonal 52" false (is_pentagonal 52)
run_test "is_pentagonal 144" false (is_pentagonal 144)
run_test "is_pentagonal 145" true (is_pentagonal 145)
run_test "is_pentagonal 146" false (is_pentagonal 146)

let main =
    time_and_save (fun() -> pentagon_numbers |> Seq.collect pentagonal_pairs_with_difference_of |> Seq.find are_sum_and_difference_pentagonal |> (fun (pk, pj) -> pj - pk))

[<STAThread>]
do main