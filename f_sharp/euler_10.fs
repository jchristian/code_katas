open System

let time work =
    let stopwatch = new System.Diagnostics.Stopwatch()
    stopwatch.Start();
    let result = work()
    printfn "It took %A ms" stopwatch.ElapsedMilliseconds
    result

let map converter list =
    let rec loop acc n = function
        | [] -> acc
        | head :: tail -> loop (converter head n :: acc) (n + 1) tail
    List.rev (loop [] 0 list)

let get_sieve_for_x_values x =
    let rec sieve (list : List<bool>) = function
        | n when n > (int (System.Math.Sqrt (float x))) -> list
        | n ->
            match list.Item(n) with
            | true -> sieve (map (fun value index -> ((index % n <> 0) && value) || (index <= n && value)) list) (n + 1)
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

let get_sum_of_primes_up_to x =
    (List.fold (fun acc (value : int) -> acc + (bigint value)) 0I (get_primes_up_to x))

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
run_test "get_primes_up_to 10" [2; 3; 5; 7] (get_primes_up_to 10)
run_test "get_primes_up_to 19" [2; 3; 5; 7; 11; 13; 17; 19] (get_primes_up_to 19)

run_test "get_sum_of_primes_up_to 10" 17I (get_sum_of_primes_up_to 10)
run_test "get_sum_of_primes_up_to 19" 77I (get_sum_of_primes_up_to 19)
printfn "%A" (time (fun() -> get_sum_of_primes_up_to 2000000))