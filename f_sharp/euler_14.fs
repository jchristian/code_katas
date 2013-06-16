let memoize func =
    let lookup_table = (new System.Collections.Generic.Dictionary<_,_>())
    fun x ->
        match (lookup_table.TryGetValue(x)) with
        | (true, value) -> value
        | _ ->
            let result = func(x)
            lookup_table.Add(x, result)
            result

let rec collatz_sequence = memoize (fun n ->
    match n with
    | n when n = 1I -> [1I]
    | _ ->
        let next_term = if n % 2I = 0I then n / 2I else 3I * n + 1I
        n :: (collatz_sequence next_term) )

let get_longest_collatz_sequence list = 
    list |> List.fold (fun (num, length) y ->
        let result = List.length (collatz_sequence y)
        if result > length then
            (y, result)
        else
            (num, length))
        (1I, 1)

let map_range map range = range |> List.map map

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

run_test "collatz_sequence 13" [13I; 40I; 20I; 10I; 5I; 16I; 8I; 4I; 2I; 1I] (collatz_sequence 13I)
run_test "collatz_sequence 13" [13I; 40I; 20I; 10I; 5I; 16I; 8I; 4I; 2I; 1I] (collatz_sequence 13I)

printfn "%A" (time (fun() -> [1I .. 1000000I] |> get_longest_collatz_sequence))