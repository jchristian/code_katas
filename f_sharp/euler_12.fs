let get_divisors x =
    let rec loop acc = function
        | cur when x = 1I -> [1I]
        | cur when cur > (bigint (System.Math.Sqrt((float x)))) -> acc
        | cur ->
            match x % cur = 0I with
            | true -> loop (cur :: (x / cur) :: acc) (cur + 1I)
            | false -> loop acc (cur + 1I)
    List.sort (loop [] 1I)

let get_nth_triangle_number n = (n * (n + 1I)) / 2I

let get_smallest_triangle_number_more_than_x_divisors x =
    let rec loop = function
        | n when List.length (get_divisors (get_nth_triangle_number n)) > x -> get_nth_triangle_number n
        | n -> loop (n + 1I)
    loop 1I

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

let time work =
    let stopwatch = new System.Diagnostics.Stopwatch()
    stopwatch.Start();
    let result = work()
    printfn "It took %A ms" stopwatch.ElapsedMilliseconds
    result

run_test "get_divisors 1" [1I] (get_divisors 1I)
run_test "get_divisors 3" [1I; 3I] (get_divisors 3I)
run_test "get_divisors 6" [1I; 2I; 3I; 6I] (get_divisors 6I)
run_test "get_divisors 10" [1I; 2I; 5I; 10I] (get_divisors 10I)
run_test "get_divisors 15" [1I; 3I; 5I; 15I] (get_divisors 15I)
run_test "get_divisors 21" [1I; 3I; 7I; 21I] (get_divisors 21I)
run_test "get_divisors 28" [1I; 2I; 4I; 7I; 14I; 28I] (get_divisors 28I)
run_test "get_divisors 29" [1I; 29I] (get_divisors 29I)

run_test "get_nth_triangle_number 1" 1I (get_nth_triangle_number 1I)
run_test "get_nth_triangle_number 2" 3I (get_nth_triangle_number 2I)
run_test "get_nth_triangle_number 7" 28I (get_nth_triangle_number 7I)

run_test "get_smallest_triangle_number_more_than_x_divisors 5" 28I (get_smallest_triangle_number_more_than_x_divisors 5)
printfn "get_smallest_triangle_number_more_than_x_divisors 500: %A" (time (fun() -> get_smallest_triangle_number_more_than_x_divisors 500))