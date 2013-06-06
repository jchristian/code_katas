let is_pythagorean_triplet (a, b, c) = (a * a) + (b * b) = (c * c)

let get_triplets =
    let rec a_loop acc = function
        | 334 -> acc
        | a ->
            let rec inner_loop acc b = function
                | c when b > c -> acc
                | c -> inner_loop ((a, b, c) :: acc) (b + 1) (c - 1)
            a_loop (inner_loop acc a (1000 - (a * 2))) (a + 1)
    a_loop [] 0

let does_sum_equal x = function
    | (a, b, c) -> a + b + c = 1000


let pythagorean_triplet_sum_equal_to_1000 =
    let rec loop = function
        | head :: tail when (is_pythagorean_triplet head) && (does_sum_equal 1000 head) -> head
        | head :: tail -> loop tail
    loop (get_triplets)

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
run_test "is_pythagorean_triplet 1 2 3" false (is_pythagorean_triplet (1, 2, 3))
run_test "is_pythagorean_triplet 3 4 5" true (is_pythagorean_triplet (3, 4, 5))
run_test "is_pythagorean_triplet 4 4 5" false (is_pythagorean_triplet (4, 4, 5))

run_test "does_sum_equal 1000 (100, 200, 300)" false (does_sum_equal 1000 (100, 200, 300))
run_test "does_sum_equal 1000 (300, 300, 400)" true (does_sum_equal 1000 (300, 300, 400))

printfn "Pythagorean triplet whose sum equals 1000: %A" (pythagorean_triplet_sum_equal_to_1000)