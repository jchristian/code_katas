open System
open System.Windows.Forms

let split (split : char) (str : string) = str.Split([| split |], System.StringSplitOptions.RemoveEmptyEntries) |> Array.toList
let parse_to_int (str : string) =
    match System.Int32.TryParse(str) with
    | (false, _) -> None
    | (true, value) -> Some(value)
let print_and_pass x = printfn "%A" x; x;

let find_maximum_sum_path triangle = 0
let load_triangle (str : string) = str |> split '\n' |> print_and_pass |> (List.map (fun x -> x |> split ' ' |> List.choose parse_to_int))

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

run_test "find_maximum_sum_path example_triangle" 23 (example_triangle |> load_triangle |> print_and_pass |> find_maximum_sum_path)

let main = printfn ""

[<STAThread>]
do main