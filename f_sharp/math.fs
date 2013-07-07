namespace Helpers.Math
    module Core =
        let get_divisors x =
            let rec loop acc = function
                | cur when x = 1I -> [1I]
                | cur when cur > (bigint (System.Math.Sqrt((float x)))) -> acc
                | cur ->
                    match x % cur = 0I with
                    | true -> loop (cur :: (x / cur) :: acc) (cur + 1I)
                    | false -> loop acc (cur + 1I)
            List.sort (loop [] 1I)

        let get_proper_divisors x =
            if x = 0I then
                []
            else
                get_divisors x |> List.rev |> List.tail |> List.rev

    module Tests =
        open Helpers.Testing
        open Core
        
        let run_tests = 
            run_test "get_divisors 0" [] (get_divisors 0I)
            run_test "get_divisors 1" [1I] (get_divisors 1I)
            run_test "get_divisors 3" [1I; 3I] (get_divisors 3I)
            run_test "get_divisors 6" [1I; 2I; 3I; 6I] (get_divisors 6I)
            run_test "get_divisors 10" [1I; 2I; 5I; 10I] (get_divisors 10I)
            run_test "get_divisors 15" [1I; 3I; 5I; 15I] (get_divisors 15I)
            run_test "get_divisors 21" [1I; 3I; 7I; 21I] (get_divisors 21I)
            run_test "get_divisors 28" [1I; 2I; 4I; 7I; 14I; 28I] (get_divisors 28I)
            run_test "get_divisors 29" [1I; 29I] (get_divisors 29I)

            run_test "get_proper_divisors 0" [] (get_proper_divisors 0I)
            run_test "get_proper_divisors 1" [] (get_proper_divisors 1I)
            run_test "get_proper_divisors 3" [1I] (get_proper_divisors 3I)
            run_test "get_proper_divisors 6" [1I; 2I; 3I] (get_proper_divisors 6I)
            run_test "get_proper_divisors 10" [1I; 2I; 5I] (get_proper_divisors 10I)
            run_test "get_proper_divisors 15" [1I; 3I; 5I] (get_proper_divisors 15I)
            run_test "get_proper_divisors 21" [1I; 3I; 7I] (get_proper_divisors 21I)
            run_test "get_proper_divisors 28" [1I; 2I; 4I; 7I; 14I] (get_proper_divisors 28I)
            run_test "get_proper_divisors 29" [1I] (get_proper_divisors 29I)