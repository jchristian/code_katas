open System
open System.Windows.Forms
open Testing

let sum_of_digits x =
    let rec loop acc = function
        | [] -> acc
        | head :: tail -> loop (acc + (System.Int32.Parse(string head))) tail
    loop 0 (Array.toList (x.ToString().ToCharArray()))

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

run_test "sum_of_digits 0" 0 (sum_of_digits 0)
run_test "sum_of_digits 123" 6 (sum_of_digits 123)
run_test "sum_of_digits 123098" 23 (sum_of_digits 123098)

let main =
    printfn "%A" (set_clipboard_value (sum_of_digits (2I ** 1000)))

[<STAThread>]
do main