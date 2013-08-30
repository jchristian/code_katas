open System
open System.Numerics
open Helpers.Testing
open Helpers.Math.Core

let is_pandigital x =
    let digits = get_digits x
    [1 .. 9] |> List.fold (fun acc x -> acc && (digits |> List.exists ((=) x))) true |> (&&) (List.length digits |> (=) 9)

let pandigital_multiples =
    let rec pandigital_multiples_up_to x (digits : array<int>) = seq {
        if not <| (digits.Length < 2) then
            let concatenated_multiples = digits |> Array.map ((*) x) |> Array.map (fun x -> x.ToString()) |> Array.fold (+) "" |> (fun s -> System.Numerics.BigInteger.TryParse(s)) |> snd

            if is_pandigital concatenated_multiples then
                yield concatenated_multiples
            if concatenated_multiples < 987654321I then
                yield! pandigital_multiples_up_to (x + 1) digits
            else
                yield! pandigital_multiples_up_to (x + 1) (digits.[0 .. digits.Length - 2]) }
    pandigital_multiples_up_to 1 [|1 .. 9|]

run_test "is_pandigital 123456789" true (is_pandigital 123456789)
run_test "is_pandigital 987654321" true (is_pandigital 987654321)
run_test "is_pandigital 987614325" true (is_pandigital 987614325)
run_test "is_pandigital 998765432" false (is_pandigital 998765432)
run_test "is_pandigital 987654311" false (is_pandigital 987654311)
run_test "is_pandigital 98765431" false (is_pandigital 98765431)
run_test "is_pandigital 9876543211" false (is_pandigital 9876543211I)

let main =
    time_and_save (fun() -> pandigital_multiples |> Seq.max)

[<STAThread>]
do main