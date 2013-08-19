namespace Helpers.Math
    module Core =
        let get_prime_factors (x : bigint) =
            let rec loop acc current = function
                | n when n > current -> List.rev acc
                | n when current % n = 0I -> loop (n :: acc) (current / n) n
                | n -> loop acc current (n + 1I)
            loop [] x 2I

        let rec get_common_values list1 list2 =
            if List.isEmpty list1 || List.isEmpty list2 then
                []
            else
                let list1_head = list1.Head
                let list2_head = list2.Head
                
                if list1_head = list2_head then
                    list1_head :: (get_common_values (list1.Tail) (list2.Tail))
                else if list1_head > list2_head then
                    get_common_values list1 (list2.Tail)
                else
                    get_common_values (list1.Tail) list2

        let simplify (n, d) =
            let prime_factors_of_n = get_prime_factors n
            let prime_factors_of_d = get_prime_factors d
            let largest_common_divisor = get_common_values prime_factors_of_n prime_factors_of_d |> List.fold (*) 1I
            (n / largest_common_divisor, d / largest_common_divisor)

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

        let is_palindrome x =
            let remove_ends (str : string) =
                str.Substring(1, ((String.length str) - 2))

            let are_ends_equal (str : string) =
                str.Substring(0, 1).Equals(str.Substring((String.length str) - 1, 1))

            let rec inner_is_palindrome = function
                | n when (String.length n) < 2 -> true
                | n when (are_ends_equal n) <> true -> false
                | n -> inner_is_palindrome (remove_ends n)
            inner_is_palindrome x

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
            run_test "get_common_values [] []" [] (get_common_values [] [])
            run_test "get_common_values [1] []" [] (get_common_values [1] [])
            run_test "get_common_values [1] [2]" [] (get_common_values [1] [2])
            run_test "get_common_values [2] [2]" [2] (get_common_values [2] [2])
            run_test "get_common_values [1; 2] [2]" [2] (get_common_values [1; 2] [2])
            run_test "get_common_values [1; 2; 2; 3] [2; 3; 4]" [2; 3] (get_common_values [1; 2; 2; 3] [2; 3; 4])
            run_test "get_common_values [1; 2; 2; 3; 5] [2; 2; 4; 5]" [2; 2; 5] (get_common_values [1; 2; 2; 3; 5] [2; 2; 4; 5])

            run_test "simplify (2, 4)" (1I, 2I) (simplify (2I, 4I))
            run_test "simplify (12, 6)" (2I, 1I) (simplify (12I, 6I))
            run_test "simplify (21, 25)" (21I, 25I) (simplify (21I, 25I))
            
            run_test "get_prime_factors 2" [2I] (get_prime_factors 2I)
            run_test "get_prime_factors 4" [2I; 2I] (get_prime_factors 4I)
            run_test "get_prime_factors 6" [2I; 3I] (get_prime_factors 6I)
            run_test "get_prime_factors 12" [2I; 2I; 3I] (get_prime_factors 12I)
            run_test "get_prime_factors 23" [23I] (get_prime_factors 23I)

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

            run_test "is_palindrome 2" true (is_palindrome "2")
            run_test "is_palindrome 20" false (is_palindrome "20")
            run_test "is_palindrome 22" true (is_palindrome "22")
            run_test "is_palindrome 202" true (is_palindrome "202")
            run_test "is_palindrome 2002" true (is_palindrome "2002")
            run_test "is_palindrome 2022" false (is_palindrome "2012")
