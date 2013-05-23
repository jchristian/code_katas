let sum = List.fold (fun acc input-> acc + input) 0 (List.filter (fun x -> x % 3 = 0 || x % 5 = 0) [1 .. 999])

printfn "%A" sum