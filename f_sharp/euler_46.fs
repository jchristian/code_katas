open System
open Helpers.Testing
open Helpers.Math.Core

let is_prime n =
  if n <= 1 then false
  elif n = 2 then true
  elif n % 2 = 0 then false
  else seq { 3 .. 2 .. int(sqrt(float(n))) } |> Seq.forall (fun i -> n % i <> 0)

let is_int x = x = (float (int64 x))

let is_twice_a_square x = is_int (sqrt(float(x) / 2.0))
let primes = Seq.initInfinite (fun x -> x) |> Seq.filter is_prime
let odd_composite_numbers = Seq.initInfinite (fun x -> x + 2) |> Seq.filter (not << is_prime) |> Seq.filter (fun x -> x % 2 = 1)

let is_sum_of_composite_and_twice_a_square x = primes |> Seq.takeWhile ((>) x) |> Seq.map ((-) x) |> Seq.exists is_twice_a_square

run_test "odd_composite_numbers |> Seq.take 10" [9; 15; 21; 25; 27; 33; 35; 39; 45; 49] (odd_composite_numbers |> Seq.take 10 |> List.ofSeq)

run_test "numbers from 1 to 100 that are twice a square" [2; 8; 18; 32; 50; 72; 98] (seq { 1 .. 100 } |> Seq.filter is_twice_a_square |> List.ofSeq)

run_test "is_sum_of_composite_and_twice_a_square 33" true (is_sum_of_composite_and_twice_a_square 33)

let main =
    time_and_save (fun() -> odd_composite_numbers |> Seq.find (not << is_sum_of_composite_and_twice_a_square))

[<STAThread>]
do main