open System
open Helpers.Testing
open Helpers.Math.Core

let pentagon_number x = x * (3 * x - 1) / 2

let pentagon_numbers =
  let rec pentagon_numbers_after x = seq { yield pentagon_number x; yield! pentagon_numbers_after (x + 1) }
  pentagon_numbers_after 1

let move_next (p_i, p_j, i, j, diff) = (p_i, pentagon_number (j + 1), i, j + 1, ((pentagon_number (j + 1)) - p_i))
let first (x, _, _, _, _) = x
let second (_, x, _, _, _) = x
let third (_, _, x, _, _) = x

let pentagon_pairs_with_differences_minimized =
  let rec loop rows = seq {
    let row_with_min_difference = rows |> Array.minBy (fun (_, _, _, _, diff) -> diff)
    let diff = row_with_min_difference |> (fun (_, _, _, _, diff) -> diff)

    let diff_of_next_row = ((rows |> Array.length) + 1) * 3 + 1
    let should_add_new_row = diff > diff_of_next_row
    if should_add_new_row then
      let i = Array.length rows + 1
      let j = i + 1
      let pent_i = pentagon_number i
      let pent_j = pentagon_number j
      let next_row = (pent_i, pent_j, i, j, pent_j - pent_i)
      let new_rows = rows |> Array.append [|move_next next_row|]
      yield (first next_row, second next_row)
      yield! loop new_rows
    else
      let i = rows |> Array.findIndex (fun row -> (third row = third row_with_min_difference))
      yield (first (rows.[i]), second (rows.[i]))
      rows.[i] <- move_next (rows.[i])
      yield! loop rows
  }

  let pent_1 = pentagon_number 1
  let pent_2 = pentagon_number 2
  loop [|(pent_1, pent_2, 1, 2, pent_2 - pent_1)|]

run_test "Seq.take 9 pentagon_pairs_with_differences_minimized" [(1, 5); (5, 12); (12, 22); (1, 12); (22, 35); (35, 51); (5, 22); (51, 70); (1, 22);] (Seq.take 9 pentagon_pairs_with_differences_minimized |> Seq.toList)

let main =
    time_and_save (fun() -> 1)

[<STAThread>]
do main