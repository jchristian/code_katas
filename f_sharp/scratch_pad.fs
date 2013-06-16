let memoize func =
    let lookup_table = (new System.Collections.Generic.Dictionary<_,_>())
    fun x ->
        match (lookup_table.TryGetValue(x)) with
        | (true, value) -> value
        | _ ->
            let result = func(x)
            lookup_table.Add(x, result)
            result

let double x = x * 2
let memoized_double = memoize double

printfn "%A" (memoized_double 2)
printfn "%A" (memoized_double 4)
printfn "%A" (memoized_double 2)

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual