let pair list =
    let rec loop acc = function
        | list when (List.length list) = 0 -> acc
        | head :: second :: tail -> loop ((head, second) :: acc) tail
    List.rev (loop [] list)

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

run_test "pair []" [] (pair [])
run_test "pair [1; 2]" [(1, 2)] (pair [1; 2])
run_test "pair [1 .. 10]" [for x in 1 .. 5 do yield (x * 2 - 1, x * 2)] (pair [1 .. 10])
run_test "pair ['a' .. 'f']" [('a', 'b'); ('c', 'd'); ('e', 'f'); ] (pair ['a' .. 'f'])