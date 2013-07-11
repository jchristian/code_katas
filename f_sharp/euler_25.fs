open System
open Helpers.Testing
open Helpers.Math.Core

let fib =
    let rec loop prev cur = seq {
        yield cur;
        yield! loop cur (prev + cur)
    }
    loop 0I 1I

run_test "fib 1" 1I (fib |> Seq.nth 0)
run_test "fib 2" 1I (fib |> Seq.nth 1)
run_test "fib 3" 2I (fib |> Seq.nth 2)
run_test "fib 4" 3I (fib |> Seq.nth 3)
run_test "fib 5" 5I (fib |> Seq.nth 4)
run_test "fib 12" 144I (fib |> Seq.nth 11)

let main =
    time_and_save (fun() -> fib |> Seq.findIndex (fun x -> ((x.ToString()).ToCharArray()) |> Array.length >= 1000) |> (fun x -> x + 1))

[<STAThread>]
do main