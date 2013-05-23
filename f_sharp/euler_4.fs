let remove_ends (str : string) =
    str.Substring(1, ((String.length str) - 2))

let are_ends_equal (str : string) =
    str.Substring(0, 1).Equals(str.Substring((String.length str) - 1, 1))

let is_palindrome x =
    let rec is_palindrome_for_string = function
        | n when (String.length n) < 2 -> true
        | n when (are_ends_equal n) <> true -> false
        | n -> is_palindrome_for_string (remove_ends n)
    is_palindrome_for_string (x.ToString())

let largest_palindrome_product limit =
    let rec loop largest = function
        | x when x > limit -> largest
        | x ->
            let rec inner_loop largest = function
                | y when y > limit -> largest
                | y when (is_palindrome (x * y)) && (x * y) > largest -> inner_loop (x * y) (y + 1I)
                | y -> inner_loop largest (y + 1I)
            loop (inner_loop largest x) (x + 1I)
    loop 0I 0I

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
run_test "remove_ends 21" "" (remove_ends "21")
run_test "remove_ends 212" "1" (remove_ends "212")
run_test "remove_ends 2342" "34" (remove_ends "2342")
run_test "remove_ends 23432" "343" (remove_ends "23432")
run_test "remove_ends 1234567654321" "23456765432" (remove_ends "1234567654321")

run_test "are_ends_equal 21" false (are_ends_equal "21")
run_test "are_ends_equal 22" true (are_ends_equal "22")
run_test "are_ends_equal 20192812" true (are_ends_equal "20192812")
run_test "are_ends_equal 20192813" false (are_ends_equal "20192813")

run_test "is_palindrome 2" true (is_palindrome 2I)
run_test "is_palindrome 20" false (is_palindrome 20I)
run_test "is_palindrome 22" true (is_palindrome 22I)
run_test "is_palindrome 202" true (is_palindrome 202I)
run_test "is_palindrome 2002" true (is_palindrome 2002I)
run_test "is_palindrome 2022" false (is_palindrome 2012I)

run_test "largest_palindrome_product 11" 121I (largest_palindrome_product 11I)
run_test "largest_palindrome_product 99" 9009I (largest_palindrome_product 99I)

printfn "largest_palindrome_product 999 - %A" (largest_palindrome_product 999I)
