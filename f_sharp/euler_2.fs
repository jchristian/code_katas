// Accumulate the answer as we go
let fibs_not_exceeding limit =
    let rec loop fibn1 fibn2 = function
        | acc when fibn1 > limit -> acc
        | acc when fibn1 % 2 = 0 -> loop (fibn1 + fibn2) fibn1 (acc + fibn1)
        | acc -> loop (fibn1 + fibn2) fibn1 acc
    loop 0 1 0

printfn "%A" (fibs_not_exceeding 4000000)