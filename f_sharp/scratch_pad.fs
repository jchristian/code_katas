let filter predicate list =
    let rec loop acc = function
        | [] -> acc
        | head :: tail ->
            match predicate head with
            | true -> loop (head :: acc) tail
            | false -> loop acc tail
    List.rev (loop [] list)

printfn "%A" (filter (fun x -> x % 2 = 0) [1 .. 10])