open System
open System.Windows.Forms

let split (split : char) (str : string) = str.Split([| split |], System.StringSplitOptions.RemoveEmptyEntries) |> Array.toList
let parse_to_int (str : string) =
    match System.Int32.TryParse(str) with
    | (false, _) -> None
    | (true, value) -> Some(value)
let print x = printfn "%A" x; x;

let find_maximum_sum_path (triangle : List<List<int>>)=
    let rec generate_next_row previous_row previous_cell acc = function
        | [] -> acc
        | head :: tail ->
            let next_cell = if List.isEmpty previous_row then 0 else previous_row.Head
            let max_cell = if next_cell > previous_cell then next_cell else previous_cell
            generate_next_row (previous_row.Tail) next_cell (max_cell + head :: acc) tail
    let last_row = //triangle |> List.fold (fun previous_row current_row -> generate_next_row previous_row 0 [] current_row)
    last_row |> List.fold (fun x y -> if x > y then x else y) 0

let load_triangle (str : string) = str |> split '\n' |> print |> (List.map (fun x -> x |> split ' ' |> List.choose parse_to_int))

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

let time work =
    let stopwatch = new System.Diagnostics.Stopwatch()
    stopwatch.Start();
    let result = work()
    printfn "It took %A ms" stopwatch.ElapsedMilliseconds
    result

let set_clipboard_value x = System.Windows.Forms.Clipboard.SetText((x.ToString())); x
let time_and_save work = set_clipboard_value (time work)

let example_triangle = "
3
7 4
2 4 6
8 5 9 3"

run_test "find_maximum_sum_path example_triangle" 23 (example_triangle |> load_triangle |> find_maximum_sum_path)

let main = printfn ""

[<STAThread>]
do main