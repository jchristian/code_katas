open System
open System.Windows.Forms

let third (_, _, c) = c

let days_in_month_excluding_feb =
    Map.ofList [(1, 31); (3, 31); (4, 30); (5, 31); (6, 30); (7, 31); (8, 31); (9, 30); (10, 31); (11, 30); (12, 31)]

let days_in_month month year =
    let get_days_in_month_of_feb year = if year % 4 = 0 && (year % 100 <> 0 || year % 400 = 0) then 29 else 28
    let days_in_month_map = days_in_month_excluding_feb |> Map.map (fun key value -> (fun year -> value)) |> Map.add 2 get_days_in_month_of_feb
    (days_in_month_map |> Map.find month) year

let sundays_in_the_first_month_of_every_year_in_the_20th_century =
    let rec loop day month year day_of_week acc =
        if day = 1 && month = 1 && year = 2001 then
            acc
        else
            let next_acc = if day_of_week = 0 && day = 1 then printfn "%A %A %A" day month year; acc + 1 else acc
            let days_in_this_month = days_in_month month year
            let next_day = if days_in_this_month = day then 1 else day + 1
            let next_month = if next_day <> 1 then month else if month = 12 then 1 else month + 1
            let next_year = if next_month = 1 && next_day = 1 then year + 1 else year
            let next_day_of_week = (day_of_week + 1) % 7

            loop next_day next_month next_year next_day_of_week next_acc
    loop 1 1 1901 2 0

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

run_test "days_in_month 1 1900" 31 (days_in_month 1 1900)
run_test "days_in_month 2 1900" 28 (days_in_month 2 1900)
run_test "days_in_month 2 1901" 28 (days_in_month 2 1901)
run_test "days_in_month 2 1904" 29 (days_in_month 2 1904)
run_test "days_in_month 4 2000" 30 (days_in_month 4 2000)
run_test "days_in_month 2 2000" 29 (days_in_month 2 2000)

let main = time_and_save (fun() -> sundays_in_the_first_month_of_every_year_in_the_20th_century)

[<STAThread>]
do main