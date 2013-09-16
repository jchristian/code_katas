open System
open Helpers.Testing
open Helpers.Math.Core

let load_words =
    let words = (System.IO.File.ReadLines("words.txt")) |> Seq.head
    (words.Split(',')) |> Array.map (fun word -> word.Substring(1, (word.Length) - 2)) |> Array.map (fun word -> word.ToLower()) |> Array.sort |> Array.toList

let get_alphabetical_value_of_letter letter = (['a' .. 'z'] |> List.findIndex (fun x -> x = letter)) + 1
let get_alphabetical_value (word : string) = (word.ToCharArray()) |> Array.map get_alphabetical_value_of_letter |> Array.reduce (+)

let is_integer x = x = (float (int x))
let triangle_index x = (Math.Sqrt(8.0 * (float x) + 1.0) - 1.0) * 0.5
let is_triangle_number x = x |> triangle_index |> is_integer

run_test "get_alphabetical_value_of_letter 'a'" 1 (get_alphabetical_value_of_letter 'a')
run_test "get_alphabetical_value_of_letter 'z'" 26 (get_alphabetical_value_of_letter 'z')

run_test "get_alphabetical_value \"colin\"" 53 (get_alphabetical_value "colin")

run_test "triangle_index 1" 1.0 (triangle_index 1)
run_test "triangle_index 3" 2.0 (triangle_index 3)
run_test "triangle_index 6" 3.0 (triangle_index 6)
run_test "triangle_index 10" 4.0 (triangle_index 10)
run_test "triangle_index 11" (0.5 * (Math.Sqrt(89.0) - 1.0)) (triangle_index 11)

run_test "is_triangle_number 1" true (is_triangle_number 1)
run_test "is_triangle_number 3" true (is_triangle_number 3)
run_test "is_triangle_number 6" true (is_triangle_number 6)
run_test "is_triangle_number 10" true (is_triangle_number 10)
run_test "is_triangle_number 11" false (is_triangle_number 11)

let main =
    time_and_save (fun() -> load_words |> List.map get_alphabetical_value |> List.filter is_triangle_number |> List.length)

[<STAThread>]
do main