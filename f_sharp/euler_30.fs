open System
open Helpers.Testing
open Helpers.Math.Core

let get_digits (x : int) = (x.ToString().ToCharArray()) |> Array.map string |> Array.map (fun (s : string) -> Convert.ToInt32(s)) |> Array.toList
let fifth_power_sum_of_digits x = x |> get_digits |> List.map (fun y -> (bigint y) ** 5) |> List.reduce (+) |> int
let is_fifth_power_sum_equal_to_x x = x |> fifth_power_sum_of_digits |> (=) x
let get_all_fifth_power_sum_equal_to_x =
    let rec loop acc = function
        | x when x > (int ((9I ** 5) * 5I)) -> acc
        | x ->
            let new_acc = if is_fifth_power_sum_equal_to_x x then x :: acc else acc
            loop new_acc (x + 1)
    loop [] 2


run_test "get_digits 5" [5] (get_digits 5)
run_test "get_digits 15" [1; 5] (get_digits 15)
run_test "get_digits 159" [1; 5; 9] (get_digits 159)

run_test "fifth_power_sum_of_digits 5" (int (5I ** 5)) (fifth_power_sum_of_digits 5)
run_test "fifth_power_sum_of_digits 15" (int (1I ** 5 + 5I ** 5)) (fifth_power_sum_of_digits 15)
run_test "fifth_power_sum_of_digits 159" (int (1I ** 5 + 5I ** 5 + 9I ** 5)) (fifth_power_sum_of_digits 159)

let main =
    time_and_save (fun() -> get_all_fifth_power_sum_equal_to_x |> List.reduce (+))

[<STAThread>]
do main