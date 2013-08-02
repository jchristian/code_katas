open System
open Helpers.Testing
open Helpers.Math.Core

let get_digits x = ((x.ToString()).ToCharArray()) |> Array.map string |> Array.map int |> Array.toList

let is_pandigital digits =
    let rec loop remaining = function
        | [] -> List.isEmpty remaining
        | head :: tail ->
            match remaining |> List.exists ((=) head) with
            | true -> loop (remaining |> List.filter ((<>) head)) tail
            | false -> false
    loop [1 .. 9] digits

let get_pandigitals =
    let rec outer_loop acc = function
        | x when x > 31622 -> acc
        | x ->
            let rec inner_loop acc = function
                | (y : int) ->
                    let product = (bigint x) * (bigint y)
                    let digits = (get_digits product) |> List.append (get_digits x) |> List.append (get_digits y)
                    let new_acc = if is_pandigital digits then (x, y, product) :: acc else acc

                    if digits |> List.length > 9 then
                        outer_loop new_acc (x + 1)
                    else
                        inner_loop new_acc (y + 1)
            inner_loop acc 1
    outer_loop [] 1

run_test "get_digits 12345" [1; 2; 3; 4; 5] (get_digits 12345)

run_test "is_pandigital 123456789" true (is_pandigital (get_digits 123456789))
run_test "is_pandigital 987654321" true (is_pandigital (get_digits 987654321))
run_test "is_pandigital 987614325" true (is_pandigital (get_digits 987614325))
run_test "is_pandigital 998765432" false (is_pandigital (get_digits 998765432))
run_test "is_pandigital 987654311" false (is_pandigital (get_digits 987654311))
run_test "is_pandigital 98765431" false (is_pandigital (get_digits 98765431))
run_test "is_pandigital 9876543211" false (is_pandigital (get_digits 9876543211I))

let main =
    time_and_save (fun() -> get_pandigitals |> List.map (fun (_, _, product) -> product) |> List.toSeq |> Seq.distinct |> Seq.reduce (+))

[<STAThread>]
do main