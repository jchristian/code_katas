open System
open Helpers.Testing
open Helpers.Math.Core

let ip f y = fun x -> f x y

let champ_seq =
    let rec loop prev = seq { yield! (prev.ToString()).ToCharArray() |> Array.map (string >> int) |> Seq.ofArray; yield! loop (prev + 1) }
    loop 0

let main =
    time_and_save (fun() -> seq { 0 .. 6 } |> Seq.map ((ip Seq.nth champ_seq) << (pown 10)) |> Seq.reduce (*))

[<STAThread>]
do main