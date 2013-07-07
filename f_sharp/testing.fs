namespace Helpers
    module Testing =
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