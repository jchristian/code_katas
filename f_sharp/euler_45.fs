open System
open Helpers.Testing
open Helpers.Math.Core

let is_int x = x = (float (int64 x))

let triangle_number x = (x * (x + 1I)) / 2I
let pentagonal_index x = (1.0 + (Math.Sqrt((float x) * 24.0 + 1.0))) / 6.0
let hexagonal_index x = (1.0 + (Math.Sqrt((float x) * 8.0 + 1.0))) / 4.0

let is_pentagonal x = (pentagonal_index x) |> is_int
let is_hexagonal x = (hexagonal_index x) |> is_int

run_test "is_pentagonal 1" true (is_pentagonal 1I)
run_test "is_pentagonal 4" false (is_pentagonal 4I)
run_test "is_pentagonal 5" true (is_pentagonal 5I)
run_test "is_pentagonal 6" false (is_pentagonal 6I)
run_test "is_pentagonal 11" false (is_pentagonal 11I)
run_test "is_pentagonal 12" true (is_pentagonal 12I)
run_test "is_pentagonal 13" false (is_pentagonal 13I)
run_test "is_pentagonal 22" true (is_pentagonal 22I)
run_test "is_pentagonal 35" true (is_pentagonal 35I)
run_test "is_pentagonal 36" false (is_pentagonal 36I)

run_test "is_hexagonal 1" true (is_hexagonal 1I)
run_test "is_hexagonal 5" false (is_hexagonal 5I)
run_test "is_hexagonal 6" true (is_hexagonal 6I)
run_test "is_hexagonal 7" false (is_hexagonal 7I)
run_test "is_hexagonal 14" false (is_hexagonal 14I)
run_test "is_hexagonal 15" true (is_hexagonal 15I)
run_test "is_hexagonal 16" false (is_hexagonal 16I)
run_test "is_hexagonal 28" true (is_hexagonal 28I)
run_test "is_hexagonal 44" false (is_hexagonal 44I)
run_test "is_hexagonal 45" true (is_hexagonal 45I)

let main =
    time_and_save (fun() -> Seq.initInfinite (fun x -> (bigint x) + 286I) |> Seq.map triangle_number |> Seq.find (fun x -> (is_pentagonal x) && (is_hexagonal x)))

[<STAThread>]
do main