open System

let map converter list =
    let rec loop acc = function
        | [] -> acc
        | hd :: tl -> loop (converter hd :: acc) tl
    List.rev (loop [] list)

let get_digits x = (map (fun (digit_as_char : char) -> (System.Convert.ToInt32 (digit_as_char.ToString()))) (List.ofArray (x.ToString().ToCharArray())))

let product_of_next_5 list =
    let rec loop acc n = function
        | list when n = 0 -> acc
        | head :: tail -> loop (acc * head) (n - 1) tail
    loop 1 5 list

let greatest_product_of_5_consecutive_digits_in y =
    let rec loop acc  = function
        | list when ((List.length list) < 5) -> acc
        | list ->
            match (product_of_next_5 list) > acc with
            | true -> loop (product_of_next_5 list) list.Tail
            | false -> loop acc list.Tail
    loop 1 (get_digits y)

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

//Tests
run_test "get_digits 1" [1] (get_digits 1I)
run_test "get_digits 12" [1; 2] (get_digits 12I)
run_test "get_digits 12345654390" [1; 2; 3; 4; 5; 6; 5; 4; 3; 9; 0] (get_digits 12345654390I)

run_test "product_of_next_5 91921" 162 (product_of_next_5 (get_digits 91921I))
run_test "product_of_next_5 919218" 162 (product_of_next_5 (get_digits 919218I))
run_test "product_of_next_5 20345654390" 0 (product_of_next_5 (get_digits 20345654390I))
run_test "product_of_next_5 12345654391" 120 (product_of_next_5 (get_digits 12345654391I))

run_test "greatest_product_of_5_consecutive_digits_in 91921" 162 (greatest_product_of_5_consecutive_digits_in 91921I)
run_test "greatest_product_of_5_consecutive_digits_in 919218" 162 (greatest_product_of_5_consecutive_digits_in 919218I)
run_test "greatest_product_of_5_consecutive_digits_in 819219" 162 (greatest_product_of_5_consecutive_digits_in 819219I)
run_test "greatest_product_of_5_consecutive_digits_in 12345654390" 3240 (greatest_product_of_5_consecutive_digits_in 12345654390I)
run_test "greatest_product_of_5_consecutive_digits_in 9999012345654390" 3240 (greatest_product_of_5_consecutive_digits_in 9999012345654390I)

let problem_number = 7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450I

printfn "greatest_product_of_5_consecutive_digits_in %A: %A" problem_number (greatest_product_of_5_consecutive_digits_in problem_number)