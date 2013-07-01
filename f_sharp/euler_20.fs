open System
open System.Windows.Forms

let to_int x = Int32.Parse(string x)

let sum_of_digits x =
    ((x.ToString()).ToCharArray()) |> Array.map to_int |> Array.reduce (+)

let factorial x = [1I .. x] |> List.reduce (*)

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
let print x = printfn "%A" x; x;
let time_and_save work = work |> time |> set_clipboard_value |> print 

run_test "sum_of_digits 0" 0 (sum_of_digits 0)
run_test "sum_of_digits 123" 6 (sum_of_digits 123)
run_test "sum_of_digits 123098" 23 (sum_of_digits 123098)

let main =
    time_and_save (fun() -> 100I |> factorial |> sum_of_digits)

[<STAThread>]
do main