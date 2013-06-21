open System
open System.Windows.Forms

let rec get_next_row acc previous_value = function
    | [] -> List.rev ((previous_value * 2I) :: acc)
    | head :: tail ->
        let next_value = head + previous_value
        get_next_row (next_value :: acc) next_value tail

let get_nth_row n = [1 .. n] |> List.fold (fun x y -> get_next_row [] 0I x) [1I]

let lattice_path x y =
    if(x < y) then
        ((get_nth_row y).Item(x))
    else
        ((get_nth_row x).Item(y))

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

let main =
    run_test "get_nth_row 0" [1I] (get_nth_row 0)
    run_test "get_nth_row 1" [1I; 2I] (get_nth_row 1)
    run_test "get_nth_row 2" [1I; 3I; 6I] (get_nth_row 2)
    run_test "get_nth_row 3" [1I; 4I; 10I; 20I] (get_nth_row 3)

    run_test "lattice_path 0 1" 1I (lattice_path 0 1)
    run_test "lattice_path 0 2" 1I (lattice_path 0 2)
    run_test "lattice_path 1 1" 2I (lattice_path 1 1)
    run_test "lattice_path 1 2" 3I (lattice_path 1 2)
    run_test "lattice_path 2 2" 6I (lattice_path 2 2)
    run_test "lattice_path 4 5" 126I (lattice_path 4 5)
    run_test "lattice_path 5 4" 126I (lattice_path 5 4)

    printfn "lattice_path 20 20: %A" (time_and_save (fun() -> lattice_path 20 20))

[<STAThread>]
do main