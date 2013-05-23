let reverse l =
    let rec loop acc = function
        | [] -> acc
        | hd :: tl -> loop (hd :: acc) tl
    loop [] l

let get_difference_between a b =
    let rec loop acc a = function
        | b when (List.length a) = 0 -> acc
        | b when (List.length b) = 0 -> acc @ a
        | b when a.Head = b.Head -> loop acc a.Tail b.Tail
        | b when a.Head > b.Head -> loop acc a b.Tail
        | b when a.Head < b.Head -> loop (a.Head :: acc) a.Tail b
    List.sort (loop [] a b)

let get_prime_factorization_of x =
    let rec loop divisors cur = function
        | x when cur > x -> divisors
        | x when x % cur = 0 -> loop (cur :: divisors) cur (x / cur)
        | x -> loop divisors (cur + 1) x
    List.sort (loop [] 2 x)

let get_largest_number_divisible_by range =
    let rec loop prime_factors = function
        | range when (List.length range) = 0 -> prime_factors
        | range ->
            let prime_factors_of_current = get_prime_factorization_of range.Head
            let difference = get_difference_between prime_factors_of_current prime_factors
            loop (List.sort (difference @ prime_factors)) range.Tail
    List.fold (fun x (y : int) -> x * (bigint y)) 1I (loop [] range)

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
run_test "get_difference_between [2; 3; 5] [2; 3]" [5] (get_difference_between [2; 3; 5] [2; 3])
run_test "get_difference_between [2; 3] [2; 3; 5]" [] (get_difference_between [2; 3] [2; 3; 5])
run_test "get_difference_between [2; 2; 3] [2; 2; 2]" [3] (get_difference_between [2; 2; 3] [2; 2; 2;])
run_test "get_difference_between [2; 3; 5] [7; 11]" [2; 3; 5] (get_difference_between [2; 3; 5] [7; 11])
run_test "get_difference_between [3; 5; 7] [2; 5; 11]" [3; 7] (get_difference_between [3; 5; 7] [2; 5; 11])

run_test "get_prime_factorization_of 2" [2] (get_prime_factorization_of 2)
run_test "get_prime_factorization_of 2" [5] (get_prime_factorization_of 5)
run_test "get_prime_factorization_of 21" [3; 7] (get_prime_factorization_of 21)
run_test "get_prime_factorization_of 8" [2; 2; 2] (get_prime_factorization_of 8)
run_test "get_prime_factorization_of 24" [2; 2; 2; 3] (get_prime_factorization_of 24)

run_test "get_largest_number_divisible_by [1 .. 10]" 2520I (get_largest_number_divisible_by [1 .. 10])
printfn "get_largest_number_divisible_by [1 .. 20]: %A" (get_largest_number_divisible_by [1 .. 20])