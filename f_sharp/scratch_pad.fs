let array = Array.init 20 (fun index_x -> Array.init 20 (fun index_y -> index_x * index_y))

printfn "%A" array.[4].[11]