(* First Try

let rec fib = function
    | 0 -> 0
    | 1 -> 1
    | n -> fib (n - 1) + fib (n - 2)

let fibs_not_exceeding limit =
    let rec loop list index =
        let fib_value = fib index
        if fib_value > limit then
            list
        else
            loop (fib_value :: list) (index + 1)
    loop [] 0

let is_even x = x % 2 = 0
let sum x y = x + y

printfn "%A" (List.fold sum 0 (List.filter is_even (fibs_not_exceeding 4000000)))

*)

// Accumulate the answer as we go
let fibs_not_exceeding limit =
    let rec loop fibn1 fibn2 = function
        | acc when fibn1 > limit -> acc
        | acc when fibn1 % 2 = 0 -> loop (fibn1 + fibn2) fibn1 (acc + fibn1)
        | acc -> loop (fibn1 + fibn2) fibn1 acc
    loop 0 1 0

printfn "%A" (fibs_not_exceeding 4000000)