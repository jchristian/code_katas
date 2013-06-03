type Point(x : int, y : int) = class
    member this.X = x
    member this.Y = y
end

let is_direction_from_within_boundary (matrix : int array array) (direction : Point) (position : Point) =
    let length = 4
    ((Array.length matrix.[0]) > position.X + 1) && (position.X >= 0) && //Is starting X position within the matrix
    ((Array.length matrix) > position.Y + 1) && (position.Y >= 0) && //Is starting Y position within the matrix
    (Array.length matrix) >= (direction.Y * length + position.Y) && (direction.Y * length + position.Y) >= 0 && //Is the ending X position within the matrix
    (Array.length matrix.[position.Y]) >= (direction.X * length + position.X) && (direction.X * length + position.X) >= 0 //Is the ending Y position within the matrix

let get_values_in_direction_from (matrix : int array array) (direction : Point) (position : Point) =
    let length = 4
    let rec loop acc = function
        | n when n = length -> acc
        | n ->
            loop (matrix.[position.Y + (direction.Y * n)].[position.X + (direction.X * n)] :: acc) (n + 1)
    List.rev (loop [] 0)

let get_sums_from_point matrix directions point = directions |> List.choose (fun direction ->
    match is_direction_from_within_boundary matrix direction point with
    | true -> Some (List.reduce (fun x y -> x + y) (get_values_in_direction_from matrix direction point))
    | false -> None)

let directions = [new Point(1, 0); new Point(1, 1); new Point(0, 1); new Point(-1, 1)]

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

run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(-1, 0)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(-1, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, -1)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, -1)))

run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 0)):" true (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 2)):" true (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 2)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 4)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 3)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 0)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 1)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 1)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(0, 4)):" false (is_direction_from_within_boundary matrix (new Point(1, 0)) (new Point(1, 4)))

run_test "is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(0, 0)):" true (is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(0, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(2, 0)):" true (is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(2, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(4, 0)):" false (is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(3, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(0, 1)):" false (is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(0, 1)))
run_test "is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(1, 1)):" false (is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(1, 1)))
run_test "is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(4, 1)):" false (is_direction_from_within_boundary matrix (new Point(0, 1)) (new Point(4, 1)))

run_test "is_direction_from_within_boundary matrix (new Point(-1, 1)) (new Point(0, 0)):" false (is_direction_from_within_boundary matrix (new Point(-1, 1)) (new Point(0, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, -1)) (new Point(0, 0)):" false (is_direction_from_within_boundary matrix (new Point(1, -1)) (new Point(0, 0)))

run_test "is_direction_from_within_boundary matrix (new Point(1, 1)) (new Point(0, 0)):" true (is_direction_from_within_boundary matrix (new Point(1, 1)) (new Point(0, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 1)) (new Point(1, 0)):" false (is_direction_from_within_boundary matrix (new Point(1, 1)) (new Point(1, 0)))
run_test "is_direction_from_within_boundary matrix (new Point(1, 1)) (new Point(0, 1)):" false (is_direction_from_within_boundary matrix (new Point(1, 1)) (new Point(0, 1)))

run_test "get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 0)):" [1; 2; 3; 4] (get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 0)))
run_test "get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 2)):" [9; 10; 11; 12] (get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 2)))
run_test "get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 3)):" [13; 14; 15; 16] (get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 3)))

run_test "get_values_in_direction_from matrix (new Point(0, 1)) (new Point(0, 0)):" [1; 5; 9; 13] (get_values_in_direction_from matrix (new Point(0, 1)) (new Point(0, 0)))
run_test "get_values_in_direction_from matrix (new Point(0, 1)) (new Point(2, 0)):" [3; 7; 11; 15] (get_values_in_direction_from matrix (new Point(0, 1)) (new Point(2, 0)))
run_test "get_values_in_direction_from matrix (new Point(0, 1)) (new Point(3, 0)):" [4; 8; 12; 16] (get_values_in_direction_from matrix (new Point(0, 1)) (new Point(3, 0)))

run_test "get_values_in_direction_from matrix (new Point(1, 1)) (new Point(0, 0)):" [1; 6; 11; 16] (get_values_in_direction_from matrix (new Point(1, 1)) (new Point(0, 0)))
run_test "get_values_in_direction_from matrix (new Point(-1, 1)) (new Point(3, 0)):" [4; 7; 10; 13] (get_values_in_direction_from matrix (new Point(-1, 1)) (new Point(3, 0)))

run_test "get_sums_from_point matrix directions (new Point(0, 0)):" [10; 34; 28] (get_sums_from_point matrix directions (new Point(0, 0)))