let sum_of_squares range = List.fold (fun x y -> x + (y * y)) 0I range
let sqaure_of_sum range =
    let sum = List.fold (fun x y -> x + y) 0I range
    sum * sum

let sum_square_difference range = (sqaure_of_sum range) - (sum_of_squares range)

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
run_test "Sum square difference 1 .. 10" 2640I (sum_square_difference [1I .. 10I])
printfn "Sum square difference 1 .. 100: %A" (sum_square_difference [1I .. 100I])