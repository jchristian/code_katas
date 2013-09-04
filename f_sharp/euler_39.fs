open System
open Helpers.Testing
open Helpers.Math.Core

let get_a b c = ((pown c 2) - (pown b 2)) ** (1.0 / 2.0)

let get_floor c = int (Math.Ceiling(((Math.Pow(2.0, (1.0/2.0)))/2.0) * (double c)))

let right_triangles_with_sum_up_to x =
    let get_integral_triangles c =
        [(get_floor c) .. (c - 1)]
        |> List.map (fun b -> (get_a (double b) (double c), b, c))
        |> List.filter (fun (a, _, _) -> (Math.Ceiling(a)) = a || (Math.Floor(a)) = a)
        |> List.map (fun (a, b, c) -> ((int a), b, c))

    [1 .. x]
    |> List.collect get_integral_triangles
    |> List.filter (fun (a, b, c) -> a + b + c <= 1000)

run_test "get_floor 5" 4 (get_floor 5)
run_test "get_floor 10" 8 (get_floor 10)
run_test "get_a 4 5" 3.0 (get_a 4.0 5.0)
run_test "get_a 48 52" 20.0 (get_a 48.0 52.0)

let main =
    time_and_save (fun() -> right_triangles_with_sum_up_to 1000 |> List.map (fun (a, b, c) -> a + b + c) |> Seq.ofList |> Seq.groupBy (fun i -> i) |> Seq.map (fun (i, s) -> (i, Seq.length s)) |> Seq.maxBy (fun (i, c) -> c))

[<STAThread>]
do main