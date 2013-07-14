open System
open Helpers.Testing
open Helpers.Math.Core

let quadratic a b x = (x * x) + (a * x) + b

printfn "Generating primes..."
let primes = new System.Collections.Generic.HashSet<int>(get_primes_up_to 100000)
printfn "Finished generating primes..."

let get_consecutive_primes a b =
    let rec loop x =
        match (x, quadratic a b x) with
        | (x, quad) when (primes.Contains(quad)) |> not -> x
        | (x, quad) when (primes.Contains(quad)) -> loop (x + 1)
    loop 0

run_test "get_consecutive_primes 1 41" 40 (get_consecutive_primes 1 41)
run_test "get_consecutive_primes -79 1601" 80 (get_consecutive_primes -79 1601)

let main =
    time_and_save (fun() -> [ for a in -999 .. 999 do for b in -999 .. 2 .. 999 do yield (a, b) ] |> List.map (fun (a, b) -> (a, b, get_consecutive_primes a b)) |> List.maxBy (fun (a, b, c) -> c) |> (fun a -> printfn "%A" a; a) |> (fun (a, b, _) -> a * b))

[<STAThread>]
do main