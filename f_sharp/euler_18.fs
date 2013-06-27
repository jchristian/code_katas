open System
open System.Windows.Forms

let split (split : char) (str : string) = str.Split([| split |], System.StringSplitOptions.RemoveEmptyEntries) |> Array.toList
let parse_to_int (str : string) =
    match System.Int32.TryParse(str) with
    | (false, _) -> None
    | (true, value) -> Some(value)
let print x = printfn "%A" x; x;

let get_tail_or_empty list = if List.isEmpty list then [] else list.Tail

let find_maximum_sum_path triangle =
    let rec generate_next_row previous_row previous_cell acc = function
        | [] -> List.rev acc
        | head :: tail ->
            let next_cell = if List.isEmpty previous_row then 0 else previous_row.Head
            let max_cell = if next_cell > previous_cell then next_cell else previous_cell
            generate_next_row (get_tail_or_empty previous_row) next_cell (max_cell + head :: acc) tail
    let last_row = triangle |> List.fold (fun previous_row current_row -> generate_next_row previous_row 0 [] current_row) []
    last_row |> List.fold (fun x y -> if x > y then x else y) 0

let load_triangle (str : string) = str |> split '\n' |> (List.map (fun x -> x |> split ' ' |> List.choose parse_to_int))

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

let main =
    let triangle = "
    75
    95 64
    17 47 82
    18 35 87 10
    20 04 82 47 65
    19 01 23 75 03 34
    88 02 77 73 07 63 67
    99 65 04 28 06 16 70 92
    41 41 26 56 83 40 80 70 33
    41 48 72 33 47 32 37 16 94 29
    53 71 44 65 25 43 91 52 97 51 14
    70 11 33 28 77 73 17 78 39 68 17 57
    91 71 52 38 17 14 91 43 58 50 27 29 48
    63 66 04 68 89 53 67 30 73 16 69 87 40 31
    04 62 98 27 23 09 70 98 73 93 38 53 60 04 23"

    time_and_save (fun() -> triangle |> load_triangle |> find_maximum_sum_path |> print)

[<STAThread>]
do main