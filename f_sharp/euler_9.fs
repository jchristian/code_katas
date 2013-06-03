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

let pythagorean_triplet_sum_equal_to_1000 =
    let rec loop = function
        |

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

printfn "%A" get_triplets