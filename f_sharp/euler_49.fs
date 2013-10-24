open System
open System.Collections.Generic
open Helpers.Testing
open Helpers.Math.Core

let is_prime n =
  if n <= 1 then false
  elif n = 2 then true
  elif n % 2 = 0 then false
  else seq { 3 .. 2 .. int(sqrt(float(n))) } |> Seq.forall (fun i -> n % i <> 0)

let memoized_is_prime = memoize is_prime

let are_all_primes numbers = numbers |> List.forall memoized_is_prime
let are_permutations x y = (get_digits x |> List.sort) = (get_digits y |> List.sort)
let are_all_permutations numbers =
    if (numbers |> List.length) < 2 then
        true
    else
        let first = List.head numbers;
        numbers |> List.tail |> List.forall (are_permutations first)
let get_digits x = (x.ToString().ToCharArray()) |> List.ofArray |> List.map (string >> int)

let main =
    time_and_save (fun() ->
        let four_digit_prime_permutations =
            [2 .. (8999 / 2)]
            |> List.collect (fun step -> [1000 .. (9999 - step * 2)]
                                         |> List.filter memoized_is_prime
                                         |> List.map (fun start -> [0 .. 2] |> List.map (fun x -> start + (step * x))))
            |> List.filter are_all_primes
            |> List.filter are_all_permutations
        four_digit_prime_permutations |> List.tail |> List.head |> List.map string |> List.reduce (+))

[<STAThread>]
do main