open System
open Helpers.Testing
open Helpers.Math.Core

let load_words =
    let names = (System.IO.File.ReadLines("names.txt")) |> Seq.head
    (names.Split(',')) |> Array.map (fun name -> name.Substring(1, (name.Length) - 2)) |> Array.map (fun name -> name.ToLower()) |> Array.sort |> Array.toList

let get_alphabetical_value_of_letter letter = (['a' .. 'z'] |> List.findIndex (fun x -> x = letter)) + 1

let get_alphabetical_value (word : string) = (word.ToCharArray()) |> Array.map get_alphabetical_value_of_letter |> Array.reduce (+)

run_test "get_alphabetical_value_of_letter 'a'" 1 (get_alphabetical_value_of_letter 'a')
run_test "get_alphabetical_value_of_letter 'z'" 26 (get_alphabetical_value_of_letter 'z')

run_test "get_alphabetical_value \"colin\"" 53 (get_alphabetical_value "colin")

let main =
    time_and_save (fun() -> load_words |> List.mapi (fun i name -> (get_alphabetical_value name) * (i + 1)) |> List.reduce (+))

[<STAThread>]
do main