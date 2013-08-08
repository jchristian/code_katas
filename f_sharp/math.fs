namespace Helpers.Math
    module Core =
        let get_divisors x =
            let rec loop acc = function
                | cur when x = 1I -> [1I] |> Set.ofList
                | cur when cur > (bigint (System.Math.Sqrt((float x)))) -> acc
                | cur ->
                    match x % cur = 0I with
                    | true -> loop (acc |> Set.add cur |> Set.add (x / cur)) (cur + 1I)
                    | false -> loop acc (cur + 1I)
            (loop Set.empty 1I) |> Set.toList |> List.sort

        let get_proper_divisors x =
            if x = 0I then
                []
            else if x = 1I then
                [1I]
            else
                get_divisors x |> List.rev |> List.tail |> List.rev

        let get_sieve_for_x_values x =
            let rec sieve (list : List<bool>) = function
                | n when n > (int (System.Math.Sqrt (float x))) -> list
                | n ->
                    match list.Item(n) with
                    | true -> sieve (List.mapi (fun index value -> ((index % n <> 0) && value) || (index <= n && value)) list) (n + 1)
                    | false -> sieve list (n + 1)
            (sieve (List.init (x + 1) (fun x -> true)) 2).Tail.Tail

        let get_primes_up_to x =
            List.rev
                (List.fold2
                    (fun acc index value ->
                        match value with
                        | true -> index :: acc
                        | false -> acc)
                    []
                    [2 .. x]
                    (get_sieve_for_x_values x)
                )

        let bigint (x : int) = bigint x
        let factorial x = [1 .. x] |> List.map bigint |> List.fold (*) 1I

        let get_digits x = ((x.ToString()).ToCharArray()) |> Array.map string |> Array.map int |> Array.toList

        let memoize func =
            let lookup_table = (new System.Collections.Generic.Dictionary<_,_>())
            fun x ->
                match (lookup_table.TryGetValue(x)) with
                | (true, value) -> value
                | _ ->
                    let result = func(x)
                    lookup_table.Add(x, result)
                    result

    module Tests =
        open Helpers.Testing
        open Core
        
        let run_tests = 
            run_test "get_divisors 0" [] (get_divisors 0I)
            run_test "get_divisors 1" [1I] (get_divisors 1I)
            run_test "get_divisors 3" [1I; 3I] (get_divisors 3I)
            run_test "get_divisors 4" [1I; 2I; 4I] (get_divisors 4I)
            run_test "get_divisors 6" [1I; 2I; 3I; 6I] (get_divisors 6I)
            run_test "get_divisors 10" [1I; 2I; 5I; 10I] (get_divisors 10I)
            run_test "get_divisors 15" [1I; 3I; 5I; 15I] (get_divisors 15I)
            run_test "get_divisors 21" [1I; 3I; 7I; 21I] (get_divisors 21I)
            run_test "get_divisors 28" [1I; 2I; 4I; 7I; 14I; 28I] (get_divisors 28I)
            run_test "get_divisors 29" [1I; 29I] (get_divisors 29I)

            run_test "get_proper_divisors 0" [] (get_proper_divisors 0I)
            run_test "get_proper_divisors 1" [1I] (get_proper_divisors 1I)
            run_test "get_proper_divisors 3" [1I] (get_proper_divisors 3I)
            run_test "get_proper_divisors 4" [1I; 2I] (get_proper_divisors 4I)
            run_test "get_proper_divisors 6" [1I; 2I; 3I] (get_proper_divisors 6I)
            run_test "get_proper_divisors 10" [1I; 2I; 5I] (get_proper_divisors 10I)
            run_test "get_proper_divisors 15" [1I; 3I; 5I] (get_proper_divisors 15I)
            run_test "get_proper_divisors 21" [1I; 3I; 7I] (get_proper_divisors 21I)
            run_test "get_proper_divisors 28" [1I; 2I; 4I; 7I; 14I] (get_proper_divisors 28I)
            run_test "get_proper_divisors 29" [1I] (get_proper_divisors 29I)

            run_test "get_primes_up_to 10" [2; 3; 5; 7] (get_primes_up_to 10)
            run_test "get_primes_up_to 19" [2; 3; 5; 7; 11; 13; 17; 19] (get_primes_up_to 19)

            run_test "factorial 0" 1I (factorial 0)
            run_test "factorial 4" 24I (factorial 4)
            run_test "factorial 20" 2432902008176640000I (factorial 20)

            run_test "get_digits 12345" [1; 2; 3; 4; 5] (get_digits 12345)
