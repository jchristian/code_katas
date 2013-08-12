open System
open Helpers.Testing
open Helpers.Math.Core

let cancel_diagonals n d =
    let n_digits = get_digits n
    let d_digits = get_digits d

    let first_diagonal = if n_digits.[1] = d_digits.[0] then [(n_digits.[0], d_digits.[1])] else []
    let second_diagonal = if n_digits.[0] = d_digits.[1] then [(n_digits.[1], d_digits.[0])] else []
    
    first_diagonal |> List.append second_diagonal

let safe_divide n d = if d = 0.0 then 0.0 else n / d
let is_curious x y =
    let diagonals = cancel_diagonals x y
    let is_any_diagonal_curious = diagonals |> List.exists (fun (n, d) -> (safe_divide (double x) (double y)) = (safe_divide (double n) (double d)))
    let is_trivial = x = y
    is_any_diagonal_curious && not is_trivial

let curious_fractions = [for x in 10I .. 99I do for y in 10I .. 99I -> (x, y)] |> List.filter (fun (n, d) -> is_curious n d && n < d)

run_test "cancel_diagonals 19 82" [] (cancel_diagonals 19 82)
run_test "cancel_diagonals 18 84" [(1, 4)] (cancel_diagonals 18 84)
run_test "cancel_diagonals 81 48" [(1, 4)] (cancel_diagonals 81 48)
run_test "cancel_diagonals 18 81" [(8, 8); (1, 1)] (cancel_diagonals 18 81)

run_test "is_curious 49 98" true (is_curious 49I 98I)
run_test "is_curious 11 22" false (is_curious 11I 11I)
run_test "is_curious 10 55" false (is_curious 10I 55I)
run_test "is_curious 42 99" false (is_curious 96I 16I)

let main =
    time_and_save (fun() -> curious_fractions |> List.reduce (fun (n1, d1) (n2, d2) -> (n1 * n2, d1 * d2)) |> simplify |> snd)

[<STAThread>]
do main