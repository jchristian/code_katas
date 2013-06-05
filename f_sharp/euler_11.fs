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

let get_product_from_point matrix directions point = directions |> List.choose (fun direction ->
    match is_direction_from_within_boundary matrix direction point with
    | true -> Some (List.reduce (fun x y -> x * y) (get_values_in_direction_from matrix direction point))
    | false -> None)

let get_greatest_product_from_point matrix directions point =
    (get_product_from_point matrix directions point) |> List.fold (fun x y -> if x > y then x else y) 0

let directions = [new Point(1, 0); new Point(1, 1); new Point(0, 1); new Point(-1, 1)]

let get_greatest_product_of_direction_in_matrix matrix =
    let rec outer_loop acc = function
        | x when x = (Array.length matrix) -> acc
        | x ->
            let rec inner_loop acc = function
                | y when y = (Array.length matrix.[x]) -> acc
                | y ->
                    match get_greatest_product_from_point matrix directions (new Point(x, y)) with
                    | sum when sum > acc -> inner_loop sum (y + 1)
                    | sum -> inner_loop acc (y + 1)
            outer_loop (inner_loop acc 0) (x + 1)
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

run_test "get_products_from_point matrix directions (0, 0):" [24; 1056; 585] (get_product_from_point matrix directions (new Point(0, 0)))
run_test "get_products_from_point matrix directions (3, 3):" [] (get_product_from_point matrix directions (new Point(3, 3)))
run_test "get_products_from_point matrix directions (3, 0):" [6144; 3640] (get_product_from_point matrix directions (new Point(3, 0)))

run_test "get_greatest_product_of_direction_in_matrix matrix" 43680 (get_greatest_product_of_direction_in_matrix matrix)

let matrix2 = [|
                 [| 10; 20; 30; 40; 1;  22 |];
                 [| 5;  6;  7;  8;  10; 30 |];
                 [| 9;  10; 90; 12; 11; 20 |];
                 [| 13; 14; 15; 80; 12; 31 |];
                 [| 13; 14; 15; 16; 12; 31 |];
                 [| 13; 14; 15; 16; 12; 31 |];
              |]
run_test "get_greatest_product_of_direction_in_matrix matrix2" 2678400 (get_greatest_product_of_direction_in_matrix matrix2)

let matrix3 = [|
                 [| 10; 2;  3;  4 |];
                 [| 5;  90; 7;  8 |];
                 [| 9;  10; 30; 12 |];
                 [| 13; 14; 15; 16 |];
              |]
run_test "get_greatest_product_of_direction_in_matrix matrix3" 432000 (get_greatest_product_of_direction_in_matrix matrix3)

let problem_matrix = [|
                        [| 8;2;22;97;38;15;0;40;0;75;4;5;7;78;52;12;50;77;91;8 |];
                        [| 49;49;99;40;17;81;18;57;60;87;17;40;98;43;69;48;4;56;62;0 |];
                        [| 81;49;31;73;55;79;14;29;93;71;40;67;53;88;30;3;49;13;36;65 |];
                        [| 52;70;95;23;4;60;11;42;69;24;68;56;1;32;56;71;37;2;36;91 |];
                        [| 22;31;16;71;51;67;63;89;41;92;36;54;22;40;40;28;66;33;13;80 |];
                        [| 24;47;32;60;99;3;45;2;44;75;33;53;78;36;84;20;35;17;12;50 |];
                        [| 32;98;81;28;64;23;67;10;26;38;40;67;59;54;70;66;18;38;64;70 |];
                        [| 67;26;20;68;2;62;12;20;95;63;94;39;63;8;40;91;66;49;94;21 |];
                        [| 24;55;58;5;66;73;99;26;97;17;78;78;96;83;14;88;34;89;63;72 |];
                        [| 21;36;23;9;75;0;76;44;20;45;35;14;0;61;33;97;34;31;33;95 |];
                        [| 78;17;53;28;22;75;31;67;15;94;3;80;4;62;16;14;9;53;56;92 |];
                        [| 16;39;5;42;96;35;31;47;55;58;88;24;0;17;54;24;36;29;85;57 |];
                        [| 86;56;0;48;35;71;89;7;5;44;44;37;44;60;21;58;51;54;17;58 |];
                        [| 19;80;81;68;5;94;47;69;28;73;92;13;86;52;17;77;4;89;55;40 |];
                        [| 4;52;8;83;97;35;99;16;7;97;57;32;16;26;26;79;33;27;98;66 |];
                        [| 88;36;68;87;57;62;20;72;3;46;33;67;46;55;12;32;63;93;53;69 |];
                        [| 4;42;16;73;38;25;39;11;24;94;72;18;8;46;29;32;40;62;76;36 |];
                        [| 20;69;36;41;72;30;23;88;34;62;99;69;82;67;59;85;74;4;36;16 |];
                        [| 20;73;35;29;78;31;90;1;74;31;49;71;48;86;81;16;23;57;5;54 |];
                        [| 1;70;54;71;83;51;54;69;16;92;33;48;61;43;52;1;89;19;67;48 |];
                     |]

printfn "get_greatest_product_of_direction_in_matrix: %A" (get_greatest_product_of_direction_in_matrix problem_matrix)