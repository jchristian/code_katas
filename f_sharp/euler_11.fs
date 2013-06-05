type Point(x : int, y : int) = class
    member this.X = x
    member this.Y = y
    override this.ToString() = (System.String.Format("({0}, {1})", this.X, this.Y))
end

let is_direction_from_within_boundary (matrix : int array array) (direction : Point) (position : Point) =
    let length = 4
    ((Array.length matrix.[0]) >= position.X + 1) && (position.X >= 0) && //Is starting X position within the matrix
    ((Array.length matrix) >= position.Y + 1) && (position.Y >= 0) && //Is starting Y position within the matrix
    (Array.length matrix.[position.Y]) >= (direction.X * length + position.X) && (direction.X * length + position.X + 1) >= 0 && //Is the ending X position within the matrix
    (Array.length matrix) >= (direction.Y * length + position.Y) && (direction.Y * length + position.Y + 1) >= 0 //Is the ending Y position within the matrix

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

let get_greatest_sum_from_point matrix directions point =
    (get_sums_from_point matrix directions point) |> List.fold (fun x y -> if x > y then x else y) 0

let directions = [new Point(1, 0); new Point(1, 1); new Point(0, 1); new Point(-1, 1)]

let get_greatest_sum_of_direction_in_matrix matrix =
    let rec outer_loop acc = function
        | x when x = (Array.length matrix) -> acc
        | x ->
            let rec inner_loop acc = function
                | y when y = (Array.length matrix.[x]) -> acc
                | y ->
                    match get_greatest_sum_from_point matrix directions (new Point(x, y)) with
                    | sum when sum > acc -> inner_loop sum (y + 1)
                    | sum -> inner_loop acc (y + 1)
            inner_loop acc 0
    outer_loop 0 0

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

let all_directions = [new Point(1, 0); new Point(1, 1); new Point(0, 1); new Point(-1, 1); new Point(-1, 0); new Point(-1, -1); new Point(0, -1); new Point(1, -1)]

let run_tests_in_multiple_directions test directions truths =
    List.iter2 test directions truths

let run_test_in_direction func func_name direction truth point =
    run_test (System.String.Format("{0} matrix {1} {2}:", func_name, direction, point)) truth (func matrix direction point)

let run_test_for_is_direction_within = run_test_in_direction is_direction_from_within_boundary "is_direction_from_within_boundary"
run_tests_in_multiple_directions (fun direction truth -> run_test_for_is_direction_within direction truth (new Point(0, 0))) all_directions [true; true; true; false; false; false; false; false]
run_tests_in_multiple_directions (fun direction truth -> run_test_for_is_direction_within direction truth (new Point(3, 0))) all_directions [false; false; true; true; true; false; false; false]
run_tests_in_multiple_directions (fun direction truth -> run_test_for_is_direction_within direction truth (new Point(3, 3))) all_directions [false; false; false; false; true; true; true; false]
run_tests_in_multiple_directions (fun direction truth -> run_test_for_is_direction_within direction truth (new Point(0, 3))) all_directions [true; false; false; false; false; false; true; true]
run_tests_in_multiple_directions (fun direction truth -> run_test_for_is_direction_within direction truth (new Point(1, 1))) all_directions [false; false; false; false; false; false; false; false]

run_test "get_values_in_direction_from matrix (1, 0) (0, 0):" [1; 2; 3; 4] (get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 0)))
run_test "get_values_in_direction_from matrix (1, 0) (0, 2):" [9; 10; 11; 12] (get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 2)))
run_test "get_values_in_direction_from matrix (1, 0) (0, 3):" [13; 14; 15; 16] (get_values_in_direction_from matrix (new Point(1, 0)) (new Point(0, 3)))

run_test "get_values_in_direction_from matrix (0, 1) (0, 0):" [1; 5; 9; 13] (get_values_in_direction_from matrix (new Point(0, 1)) (new Point(0, 0)))
run_test "get_values_in_direction_from matrix (0, 1) (2, 0):" [3; 7; 11; 15] (get_values_in_direction_from matrix (new Point(0, 1)) (new Point(2, 0)))
run_test "get_values_in_direction_from matrix (0, 1) (3, 0):" [4; 8; 12; 16] (get_values_in_direction_from matrix (new Point(0, 1)) (new Point(3, 0)))

run_test "get_values_in_direction_from matrix ( 1, 1) (0, 0):" [1; 6; 11; 16] (get_values_in_direction_from matrix (new Point(1, 1)) (new Point(0, 0)))
run_test "get_values_in_direction_from matrix (-1, 1) (3, 0):" [4; 7; 10; 13] (get_values_in_direction_from matrix (new Point(-1, 1)) (new Point(3, 0)))

run_test "get_sums_from_point matrix directions (0, 0):" [10; 34; 28] (get_sums_from_point matrix directions (new Point(0, 0)))
run_test "get_sums_from_point matrix directions (3, 3):" [] (get_sums_from_point matrix directions (new Point(3, 3)))
run_test "get_sums_from_point matrix directions (3, 0):" [40; 34] (get_sums_from_point matrix directions (new Point(3, 0)))

run_test "get_greatest_sum_of_direction_in_matrix matrix" 58 (get_greatest_sum_of_direction_in_matrix matrix)