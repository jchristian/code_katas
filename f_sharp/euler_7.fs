let rec last = function
    | hd :: [] -> hd
    | hd :: tl -> last tl
    | _ -> failwith "Empty list."

let not x = x = false

let rec is_x_divisible_by x = function
    | list when (List.length list) = 0 -> false
    | list when (x % list.Head) = 0I -> true
    | list -> is_x_divisible_by x list.Tail

let get_nth_prime limit =
    let rec loop (primes : List<bigint>) n = function
        | cur when n > limit -> (last primes)
        | cur when (not (is_x_divisible_by cur primes)) -> loop (primes @ [cur]) (n + 1I) (cur + 1I)
        | cur -> loop primes n (cur + 1I)
    loop [] 1I 2I

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
run_test "is_x_divisible_by 2 []" false (is_x_divisible_by 2I [])
run_test "is_x_divisible_by 2 [2]" true (is_x_divisible_by 2I [2I])
run_test "is_x_divisible_by 9 [2; 3]" true (is_x_divisible_by 9I [2I; 3I])
run_test "is_x_divisible_by 9 [3]" true (is_x_divisible_by 9I [3I])
run_test "is_x_divisible_by 21 [2; 3; 5]" true (is_x_divisible_by 21I [2I; 3I; 5I])
run_test "is_x_divisible_by 53 [2; 3; 5; 7]" false (is_x_divisible_by 53I [2I; 3I; 5I; 7I])
run_test "is_x_divisible_by 100 [2; 3; 5; 7]" true (is_x_divisible_by 100I [2I; 3I; 5I; 7I])
run_test "is_x_divisible_by 121 [2; 3; 5; 7; 11]" true (is_x_divisible_by 121I [2I; 3I; 5I; 7I; 11I])

run_test "get_nth_prime 1" 2I (get_nth_prime 1I)
run_test "get_nth_prime 6" 13I (get_nth_prime 6I)
run_test "get_nth_prime 10" 29I (get_nth_prime 10I)

printfn "get_nth_prime 10001: %A" (get_nth_prime 10001I)
