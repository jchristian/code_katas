open System
open System.Collections.Generic
open Helpers.Testing
open Helpers.Math.Core

let is_prime n =
  if n <= 1 then false
  elif n = 2 then true
  elif n % 2 = 0 then false
  else seq { 3 .. 2 .. int(sqrt(float(n))) } |> Seq.forall (fun i -> n % i <> 0)

let primes = get_primes_up_to 1000000 |> List.toArray

let largest_prime_sum_of x = 
    let rec loop min max (primes : array<int>) = function
        | _ when primes.[min] > x -> [||]
        | sum when sum = x -> (primes.[min .. max])
        | sum ->
            let mutable next_min = min
            let mutable next_max = max
            if sum > x then
                next_min <- min + 1
            else
                next_max <- max + 1
            loop next_min next_max primes (primes.[next_min .. next_max] |> Array.fold (+) 0)
    loop 0 0 primes 0

run_test "largest_prime_sum_of 41" [|2; 3; 5; 7; 11; 13|] (largest_prime_sum_of 41)

let main = time_and_save (fun() -> primes
                                   |> Array.map (fun p -> (p, largest_prime_sum_of p))
                                   |> Array.map (fun (p, lps) -> (p, lps, (lps |> Array.fold (+) 0)))
                                   |> Array.maxBy (fun (_, lps, _) -> lps.Length)
                                   |> (fun (f, s, t) -> f))

[<STAThread>]
do main