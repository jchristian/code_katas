type Point(x : int, y : int) = class
    member this.X = x
    member this.Y = y
end

let is_direction_from_within_boundary (matrix : int array array) (direction : Point) (position : Point) =
    let length = 4
    ((Array.length matrix) > position.Y + 1) && (Array.length matrix.[position.Y]) >= (direction.X * length + position.X)

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
let matrix = [|
                [| 1;  2;  3;  4 |];
                [| 5;  6;  7;  8 |];
                [| 9;  10; 11; 12 |];
                [| 13; 14; 15; 16 |];
             |]

run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 0)):" true (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 2)):" true (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 2)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 4)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 3)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 0)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 1)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 1)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 4)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 4)))

//run_test "get_values_in_direction_from matrix (1, 0) (0, 0):" [1; 2; 3; 4] (get_values_in_direction_from matrix (1, 0) (0, 0))
