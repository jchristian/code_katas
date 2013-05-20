let largest_prime_factor (x : bigint) =
    let rec loop largest current = function
        | n when n > current -> largest
        | n when current % n = 0I -> loop n (current / n) n
        | n -> loop largest current (n + 1I)
    loop 1I x 2I

let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

run_test "Largest prime factor of 2" 2I (largest_prime_factor 2I)
run_test "Largest prime factor of 6" 3I (largest_prime_factor 6I)
run_test "Largest prime factor of 9" 3I (largest_prime_factor 9I)
run_test "Largest prime factor of 10" 5I (largest_prime_factor 10I)
run_test "Largest prime factor of 11" 11I (largest_prime_factor 11I)
run_test "Largest prime factor of 20" 5I (largest_prime_factor 20I)
run_test "Largest prime factor of 30" 5I (largest_prime_factor 30I)
run_test "Largest prime factor of 40" 5I (largest_prime_factor 40I)

printfn "Largest prime factor of 600851475143: %A" (largest_prime_factor 600851475143I)
