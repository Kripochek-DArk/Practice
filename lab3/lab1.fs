open System

let inputCorect romanNumber =
    match romanNumber with
    | "I" | "II" | "III" | "IV" | "V" | "VI" | "VII" | "VIII" | "IX" -> true
    | _ -> false

let romanToNumber romanNumber =
    match romanNumber with
    | "I" -> 1
    | "II" -> 2
    | "III" -> 3
    | "IV" -> 4
    | "V" -> 5
    | "VI" -> 6
    | "VII" -> 7
    | "VIII" -> 8
    | "IX" -> 9
    | _ -> 0

let inputNumbers () =
    printfn "Чтобы закончить ввод, просто нажмите Enter"

    Seq.unfold (fun () ->
        printf "Введите римское число от I до IX: "
        let romanNumber = Console.ReadLine()

        if String.IsNullOrWhiteSpace romanNumber then
            None
        elif inputCorect romanNumber then
            Some(romanNumber, ())
        else
            printfn "Введены некорректные данные"
            Some("", ())
    ) ()

[<EntryPoint>]
let main args =

    inputNumbers ()
    |> Seq.map romanToNumber
    |> Seq.iter (fun x -> printf "%d \n" x)

    printfn ""
    0