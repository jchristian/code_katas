open System
open System.Windows.Forms

let tuple_to_map map (key, value) = map |> Map.add key value

let number_to_word_table = [(0, "zero");
                            (1, "one");
                            (2, "two");
                            (3, "three");
                            (4, "four");
                            (5, "five");
                            (6, "six");
                            (7, "seven");
                            (8, "eight");
                            (9, "nine");
                            (10, "ten");
                            (11, "eleven");
                            (12, "twelve");
                            (13, "thirteen");
                            (14, "fourteen");
                            (15, "fifteen");
                            (16, "sixteen");
                            (17, "seventeen");
                            (18, "eighteen");
                            (19, "nineteen");
                            (20, "twenty");
                            (30, "thirty");
                            (40, "forty");
                            (50, "fifty");
                            (60, "sixty");
                            (70, "seventy");
                            (80, "eighty");
                            (90, "ninety");
                            (100, "hundred")] |> List.fold tuple_to_map Map.empty

let rec number_as_word = function
    | 100 -> "one hundred"
    | 1000 -> "one thousand"
    | x when number_to_word_table |> Map.containsKey x -> (number_to_word_table |> Map.find x)
    | x when x < 100 -> (number_as_word (x - (x % 10))) + "-" + (number_as_word (x % 10))
    | x when x > 100 && x % 100 = 0 -> (number_as_word (x / 100)) + " hundred"
    | x when x > 100 -> (number_as_word (x / 100)) + " hundred and " + (number_as_word (x % 100))

let number_of_letters (str : string) = (String.length ((str.Replace(" ", "")).Replace("-", "")))

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual

let time work =
    let stopwatch = new System.Diagnostics.Stopwatch()
    stopwatch.Start();
    let result = work()
    printfn "It took %A ms" stopwatch.ElapsedMilliseconds
    result

let set_clipboard_value x = System.Windows.Forms.Clipboard.SetText((x.ToString())); x
let time_and_save work = set_clipboard_value (time work)

run_test "number_as_word 0" "zero" (number_as_word 0)
run_test "number_as_word 1" "one" (number_as_word 1)
run_test "number_as_word 11" "eleven" (number_as_word 11)
run_test "number_as_word 20" "twenty" (number_as_word 20)
run_test "number_as_word 21" "twenty-one" (number_as_word 21)
run_test "number_as_word 30" "thirty" (number_as_word 30)
run_test "number_as_word 100" "one hundred" (number_as_word 100)
run_test "number_as_word 200" "two hundred" (number_as_word 200)
run_test "number_as_word 121" "one hundred and twenty-one" (number_as_word 121)
run_test "number_as_word 915" "nine hundred and fifteen" (number_as_word 915)
run_test "number_as_word 342" "three hundred and forty-two" (number_as_word 342)
run_test "number_as_word 342" 23 (342 |> number_as_word |> number_of_letters)

let main =
    printfn "%A" (time_and_save (fun() -> [1 .. 1000] |> List.fold (fun count x -> (x |> number_as_word |> number_of_letters) + count) 0))

[<STAThread>]
do main