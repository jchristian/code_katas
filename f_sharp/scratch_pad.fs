let withSideEffects x =
    x := "Side Effected"

let main() =
    let msg = ref "Not Effected"
    printfn "%A" !msg

    let setMsg () =
        msg := "set"
    setMsg()
    printfn "%A" !msg

    withSideEffects msg
    printfn "%A" !msg

main()    

//Testing functions
let pass_fail = function
    | true -> "Passed"
    | false -> "FAILED"

let run_test description expected actual =
    printfn "%s - %s: Expected <%A> Actual <%A>" (pass_fail (expected = actual)) description expected actual