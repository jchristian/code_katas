open System
open System.Windows.Forms

let days_of_month_excluding_feb =
    Map.ofList [(1, 31); (3, 31); (4, 30); (5, 31); (6, 30); (7, 31); (8, 31); (9, 30); (10, 31); (11, 30); (12, 31)]

let days_of_month month year =
    let get_days_in_month_of_feb year = if year % 4 = 0 && (year % 100 <> 0 || year % 400 = 0) then 29 else 28
    let days_of_month_map = days_of_month_excluding_feb |> Map.map (fun key value -> (fun year -> value)) |> Map.add 2 get_days_in_month_of_feb
    (days_of_month_map |> Map.find month) year

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

run_test "days_of_month 1 1900" 31 (days_of_month 1 1900)
run_test "days_of_month 2 1900" 28 (days_of_month 2 1900)
run_test "days_of_month 2 1901" 28 (days_of_month 2 1901)
run_test "days_of_month 2 1904" 29 (days_of_month 2 1904)
run_test "days_of_month 4 2000" 30 (days_of_month 4 2000)
run_test "days_of_month 2 2000" 29 (days_of_month 2 2000)

let main = printfn "Test"

[<STAThread>]
do main